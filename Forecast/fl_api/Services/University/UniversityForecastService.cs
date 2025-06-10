using fl_api.Dtos.University;
using fl_api.Interfaces.University;
using fl_api.Models.Forecast;
using University.Interfaces;

namespace fl_api.Services.University
{
    public class UniversityForecastService : IUniversityForecastService
    {
        private readonly IUniversityApiClient _api;
        private readonly IForecastSemestreRepository _repo;

        public UniversityForecastService(IUniversityApiClient api, IForecastSemestreRepository repo)
        {
            _api = api;
            _repo = repo;
        }

        public async Task<List<MovimientoDetalleDto>> GetDetallePorInsumoAsync(string insumoNombre)
        {
            var movimientos = await _api.GetMovimientosInventarioAsync();

            return movimientos
                .Where(m => string.Equals(m.InsumoNombre, insumoNombre, StringComparison.OrdinalIgnoreCase))
                .Select(m => new MovimientoDetalleDto
                {
                    FechaEntregado = m.FechaEntregado,
                    FechaDevuelto = m.FechaDevuelto,
                    Cantidad = m.Cantidad,
                    TipoMovimiento = m.TipoMovimiento,
                    Responsable = m.Responsable,
                    IdSolicitud = m.IdSolicitud
                })
                .ToList();
        }
        // FaC20PFaUr2bdaZMFrB
        public async Task<List<MovimientoResumenDto>> GetMovimientosPorSemestreAsync(string semestreId)
        {
            var movimientos = await _api.GetMovimientosInventarioAsync();

            DateTime fechaInicio, fechaFin;

            switch (semestreId)
            {
                case "2025-1":
                    fechaInicio = new DateTime(2025, 2, 1);
                    fechaFin = new DateTime(2025, 6, 30);
                    break;
                case "2025-2":
                    fechaInicio = new DateTime(2025, 8, 1);
                    fechaFin = new DateTime(2025, 12, 31);
                    break;
                default:
                    return new(); // o lanzar excepción
            }

            var filtrados = movimientos
                .Where(m => m.FechaEntregado >= fechaInicio && m.FechaEntregado <= fechaFin)
                .GroupBy(m => new { m.InsumoNombre, m.TipoMovimiento, m.Responsable })
                .Select(g => new MovimientoResumenDto
                {
                    Insumo = g.Key.InsumoNombre,
                    TipoMovimiento = g.Key.TipoMovimiento,
                    Responsable = g.Key.Responsable,
                    CantidadTotal = g.Sum(x => x.Cantidad)
                })
                .ToList();

            return filtrados;
        }

        public async Task<List<MovimientoResumenDto>> GetForecastPorRangoAsync(DateTime from, DateTime to)
        {
            var movimientos = await _api.GetMovimientosInventarioAsync();

            var filtrados = movimientos
                .Where(m =>
                    m.TipoMovimiento == "PRESTAMO" &&
                    m.FechaEntregado >= from &&
                    m.FechaEntregado <= to)
                .GroupBy(m => m.InsumoNombre)
                .Select(g => new MovimientoResumenDto
                {
                    Insumo = g.Key,
                    TipoMovimiento = "PRESTAMO",
                    Responsable = "Sistema Forecast",
                    CantidadTotal = g.Sum(x => x.Cantidad)
                })
                .ToList();

            return filtrados;
        }

        public async Task<List<ForecastSemestralDto>> GetForecastSemestralAsync()
        {
            var movimientos = await GetForecastPorRangoAsync(
                new DateTime(2025, 7, 1),
                new DateTime(2025, 12, 31)
            );

            var random = new Random();

            var forecast = movimientos
                .GroupBy(m => m.Insumo)
                .Select(g =>
                {
                    var totalPronosticado = g.Sum(x => x.CantidadTotal);
                    //var stock = random.Next(0, totalPronosticado / 2); // para asegurar unidades a comprar
                    var stock = 0; // Fuerza compra de todo

                    return new ForecastSemestralDto
                    {
                        InsumoNombre = g.Key,
                        StockActual = stock,
                        TotalPronosticadoSemestre = totalPronosticado,
                        UnidadesAComprar = Math.Max(0, totalPronosticado - stock)
                    };
                })
                .ToList();

            // Guardar en MongoDB
            var records = forecast.Select(f => new ForecastSemestreRecord
            {
                InsumoNombre = f.InsumoNombre,
                StockActual = f.StockActual,
                TotalPronosticadoSemestre = f.TotalPronosticadoSemestre,
                UnidadesAComprar = f.UnidadesAComprar
            });

            await _repo.SaveManyAsync(records);

            return forecast;
        }
    }
}
