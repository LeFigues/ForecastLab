using fl_api.Dtos.Reports;
using fl_api.Interfaces;
using fl_api.Interfaces.Reports;
using fl_api.Interfaces.University;
using fl_api.Models.Reports;
using System.Text;
using System.Text.Json;
using University.Interfaces;

namespace fl_api.Services.Reports
{
    public class ReportService : IReportService
    {
        private readonly IUniversityApiClient _university;
        private readonly IUniversityForecastService _universityForecast;
        private readonly IOpenAIService _openai;
        private readonly IPrecioEstimadoRepository _precioRepo;
        public ReportService(
            IUniversityApiClient university,
            IUniversityForecastService universityForecast,
            IOpenAIService openai,
            IPrecioEstimadoRepository precioRepo)
        {
            _university = university;
            _universityForecast = universityForecast;
            _openai = openai;
            _precioRepo = precioRepo;
        }


        public async Task<List<ConsumoVsPronosticoDto>> GetConsumoVsPronosticoAsync(ReportFilterDto filter)
        {
            // Obtener todos los movimientos
            var movimientos = await _university.GetMovimientosInventarioAsync();

            // Filtrar los movimientos tipo PRESTAMO en el rango de fechas
            var prestamos = movimientos
    .Where(m =>
        m.TipoMovimiento == "PRESTAMO" &&
        m.FechaEntregado >= filter.From &&
        m.FechaEntregado <= filter.To
    )
    .ToList();

            // 🔹 Filtro por categoría (nombre del insumo contiene categoría)
            if (!string.IsNullOrWhiteSpace(filter.Category))
            {
                prestamos = prestamos
                    .Where(m => m.InsumoNombre
                        .ToLowerInvariant()
                        .Contains(filter.Category.ToLowerInvariant()))
                    .ToList();
            }

            // 🔹 Filtro por laboratorio (si `Responsable` contiene el labId como código o nombre)
            if (!string.IsNullOrWhiteSpace(filter.LabId))
            {
                prestamos = prestamos
                    .Where(m => m.Responsable
                        .ToLowerInvariant()
                        .Contains(filter.LabId.ToLowerInvariant()))
                    .ToList();
            }


            // Agrupar por mes y nombre de insumo
            var consumoAgrupado = prestamos
                .GroupBy(m => new { Mes = m.FechaEntregado.ToString("yyyy-MM"), m.InsumoNombre })
                .Select(g => new
                {
                    g.Key.Mes,
                    g.Key.InsumoNombre,
                    Total = g.Sum(x => x.Cantidad)
                })
                .ToList();

            // TODO: reemplazar esto con tu fuente real de forecast si aún no está integrado
            var pronosticoSimulado = new List<ForecastMensual>(); // ← temporal

            var resultado = consumoAgrupado.Select(c => new ConsumoVsPronosticoDto
            {
                Month = c.Mes,
                SupplyName = c.InsumoNombre,
                RealConsumption = c.Total,
                Forecasted = pronosticoSimulado
                    .Where(f => f.InsumoNombre == c.InsumoNombre && f.PeriodStart.ToString("yyyy-MM") == c.Mes)
                    .Sum(f => f.ForecastedQuantity)
            }).ToList();

            return resultado;
        }

        public async Task<List<UnidadesAComprarDto>> GetUnidadesAComprarAsync(ReportFilterDto filter)
        {
            var forecast = await _universityForecast.GetForecastPorRangoAsync(filter.From, filter.To);
            var forecastMap = forecast.ToDictionary(f => f.Insumo, f => f.CantidadTotal);

            var insumos = await _university.GetInsumosAsync();
            // 🔹 Filtro por categoría
            if (!string.IsNullOrWhiteSpace(filter.Category))
            {
                insumos = insumos
                    .Where(i => i.Nombre
                        .ToLowerInvariant()
                        .Contains(filter.Category.ToLowerInvariant()))
                    .ToList();
            }

            // 🔹 Filtro por laboratorio (si decides usar ubicación o nombre como proxy)
            if (!string.IsNullOrWhiteSpace(filter.LabId))
            {
                insumos = insumos
                    .Where(i => i.Ubicacion
                        .ToLowerInvariant()
                        .Contains(filter.LabId.ToLowerInvariant()))
                    .ToList();
            }

            var resultado = forecastMap.Select(kvp =>
            {
                var nombre = kvp.Key;
                var forecastCantidad = kvp.Value;
                var insumo = insumos.FirstOrDefault(i => i.Nombre.Equals(nombre, StringComparison.OrdinalIgnoreCase));
                var stock = insumo?.StockActual ?? 0;

                return new UnidadesAComprarDto
                {
                    SupplyName = nombre,
                    CurrentStock = stock,
                    ForecastedDemand = forecastCantidad,
                    UnitCost = null
                };
            }).ToList();

            // 🔹 Obtener precios ya guardados en Mongo
            var preciosGuardados = await _precioRepo.GetAllAsync();
            var preciosMap = preciosGuardados.ToDictionary(p => p.Nombre, p => p.Precio);

            // 🔹 Identificar insumos sin precio
            var insumosSinPrecio = resultado
                .Where(r => !preciosMap.ContainsKey(r.SupplyName))
                .Select(r => r.SupplyName)
                .Distinct()
                .ToList();

            // 🔹 Si hay insumos sin precio → generar prompt y consultar a GPT
            if (insumosSinPrecio.Any())
            {
                var prompt = new StringBuilder();
                prompt.AppendLine("Estima el precio promedio de los siguientes insumos electrónicos. Devuelve solo la respuesta como JSON con este formato:");
                prompt.AppendLine("[");
                prompt.AppendLine("  { \"nombre\": \"Breadboard\", \"precio\": 15.50 },");
                prompt.AppendLine("  { \"nombre\": \"LED Rojo 0.5mm\", \"precio\": 0.20 }");
                prompt.AppendLine("]");
                prompt.AppendLine();
                prompt.AppendLine("Lista:");
                foreach (var nombre in insumosSinPrecio)
                    prompt.AppendLine($"- {nombre}");

                var gptResponse = await _openai.AnalyzeTextAsync(prompt.ToString(), "gpt-4o");

                var opciones = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };
                List<PrecioEstimado>? preciosGPT;
                try
                {
                    preciosGPT = JsonSerializer.Deserialize<List<PrecioEstimado>>(gptResponse, opciones);
                }
                catch
                {
                    preciosGPT = new();
                }

                // 🔹 Guardar los nuevos en Mongo
                var nuevosRegistros = (preciosGPT ?? new())
                    .Where(p => !string.IsNullOrWhiteSpace(p.Nombre) && p.Precio > 0)
                    .DistinctBy(p => p.Nombre)
                    .Select(p => new PrecioEstimadoRecord
                    {
                        Nombre = p.Nombre,
                        Precio = p.Precio
                    });

                await _precioRepo.SaveManyAsync(nuevosRegistros);

                // 🔹 Actualizar el diccionario de precios en memoria
                foreach (var p in nuevosRegistros)
                    preciosMap[p.Nombre] = p.Precio;
            }

            // 🔹 Asignar precios al resultado
            foreach (var item in resultado)
            {
                if (preciosMap.TryGetValue(item.SupplyName, out var precio))
                    item.UnitCost = precio;
            }

            return resultado;
        }

        public async Task<byte[]> ExportReportAsync(ExportRequestDto request)
        {
            // TODO: Generar archivo PDF/CSV/Excel según tipo de reporte y formato
            return Array.Empty<byte>();
        }

    }
}
