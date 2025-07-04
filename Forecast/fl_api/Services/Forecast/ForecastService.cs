﻿using fl_api.Dtos.Forecast;
using fl_api.Interfaces.IForecast;
using fl_api.Interfaces.University;
using fl_api.Models.Forecast;
using University.Interfaces;
using fl_api.Models;
using fl_api.Repositories.Forecast;

namespace fl_api.Services.Forecast
{
    public class ForecastService : IForecastService
    {
        private readonly IUniversityApiClient _api;
        private readonly IForecastRiesgoRepository _repo;
        private readonly IForecastHistoricoRepository _historicoRepo;
        private readonly IForecastPracticaRepository _practicaRepo;
        private readonly IForecastSemestreRepository _semestreRepo; // Nuevo repositorio

        // Precio unitario de ejemplo; en producción, leer de catálogo o configuración
        private readonly decimal _precioUnitario = 5.0m;

        public ForecastService(
            IUniversityApiClient api,
            IForecastRiesgoRepository riesgoRepo,
            IForecastHistoricoRepository historicoRepo,
            IForecastPracticaRepository practicaRepo,
            IForecastSemestreRepository semestreRepo // Se agrega aquí
        )
        {
            _api = api;
            _repo = riesgoRepo;
            _historicoRepo = historicoRepo;
            _practicaRepo = practicaRepo;
            _semestreRepo = semestreRepo;
        }

        public Task<List<ForecastPointDto>> ForecastAsync(
            IEnumerable<DailyDemandDto> history,
            string horizon)
        {
            // Función para agrupar en el primer día del mes o semestre
            Func<DateTime, DateTime> periodKey = horizon.ToLower() switch
            {
                "monthly" => d => new DateTime(d.Date.Year, d.Date.Month, 1),
                "semestral" => d =>
                {
                    var half = (d.Date.Month - 1) / 6;
                    return new DateTime(d.Date.Year, half * 6 + 1, 1);
                }
                ,
                _ => throw new ArgumentException("Horizon debe ser 'monthly' o 'semestral'")
            };

            // 1) Agrupamos y ordenamos
            var series = history
                .GroupBy(d => periodKey(d.Date))
                .OrderBy(g => g.Key)
                .Select((g, idx) => new { Index = idx, Period = g.Key, Total = g.Sum(x => x.Quantity) })
                .ToList();

            // 2) Si no hay nada, no hay forecast
            if (series.Count == 0)
                return Task.FromResult(new List<ForecastPointDto>());

            // 3) Determinamos cuántos pasos predecir
            int h = horizon.ToLower() == "monthly" ? 6 : 2;

            // 4) Caso especial: sólo un punto → repetimos ese valor
            if (series.Count == 1)
            {
                var single = series[0];
                var results = new List<ForecastPointDto>();
                for (int i = 1; i <= h; i++)
                {
                    DateTime next = horizon.ToLower() switch
                    {
                        "monthly" => single.Period.AddMonths(i),
                        "semestral" => single.Period.AddMonths(i * 6),
                        _ => single.Period
                    };
                    results.Add(new ForecastPointDto
                    {
                        PeriodStart = next,
                        ForecastedQuantity = single.Total
                    });
                }
                return Task.FromResult(results);
            }

            // 5) Regresión lineal si hay 2 o más puntos
            int n = series.Count;
            double sumX = series.Sum(p => p.Index);
            double sumY = series.Sum(p => p.Total);
            double sumXY = series.Sum(p => p.Index * p.Total);
            double sumX2 = series.Sum(p => p.Index * p.Index);

            double denom = n * sumX2 - sumX * sumX;
            double b = denom == 0 ? 0 : (n * sumXY - sumX * sumY) / denom;
            double a = (sumY - b * sumX) / n;

            var lastPeriod = series.Last().Period;
            var forecast = new List<ForecastPointDto>();
            for (int i = 1; i <= h; i++)
            {
                DateTime next = horizon.ToLower() switch
                {
                    "monthly" => lastPeriod.AddMonths(i),
                    "semestral" => lastPeriod.AddMonths(i * 6),
                    _ => lastPeriod
                };
                double xNext = series.Count - 1 + i;
                double yNext = a + b * xNext;
                forecast.Add(new ForecastPointDto
                {
                    PeriodStart = next,
                    ForecastedQuantity = Math.Max(0, yNext)
                });
            }
            return Task.FromResult(forecast);
        }

        public async Task<List<ForecastInsumoDto>> ForecastInsumosPorPracticaAsync()
        {
            var solicitudes = await _api.GetSolicitudesUsoAsync();
            var insumosPorPractica = await _api.GetInsumosPorPracticaAsync();

            var resultado = new List<ForecastInsumoDto>();

            foreach (var solicitud in solicitudes)
            {
                var insumos = insumosPorPractica
                    .Where(x => x.IdPractica == solicitud.IdPractica)
                    .ToList();

                foreach (var insumo in insumos)
                {
                    resultado.Add(new ForecastInsumoDto
                    {
                        PracticaTitulo = solicitud.PracticaTitulo,
                        InsumoNombre = insumo.NombreInsumo,
                        CantidadRequerida = insumo.Cantidad * solicitud.NumeroGrupos
                    });
                }
            }

            // Agrupar por insumo + práctica
            return resultado
                .GroupBy(r => new { r.PracticaTitulo, r.InsumoNombre })
                .Select(g => new ForecastInsumoDto
                {
                    PracticaTitulo = g.Key.PracticaTitulo,
                    InsumoNombre = g.Key.InsumoNombre,
                    CantidadRequerida = g.Sum(x => x.CantidadRequerida)
                })
                .ToList();
        }

        public async Task<List<ForecastHistoricoDto>> ForecastInsumosHistoricoAsync()
        {
            var movimientos = await _api.GetMovimientosInventarioAsync();

            var historial = movimientos
                .Where(m => m.TipoMovimiento.ToUpper() == "PRESTAMO")
                .GroupBy(m => new
                {
                    m.InsumoNombre,
                    Mes = m.FechaEntregado.ToString("yyyy-MM")
                })
                .Select(g => new ForecastHistoricoDto
                {
                    InsumoNombre = g.Key.InsumoNombre,
                    Mes = g.Key.Mes,
                    TotalUsado = g.Sum(x => x.Cantidad)
                })
                .OrderBy(x => x.Mes)
                .ThenBy(x => x.InsumoNombre)
                .ToList();

            var records = historial.Select(h => new ForecastHistoricoRecord
            {
                InsumoNombre = h.InsumoNombre,
                Mes = h.Mes,
                TotalUsado = h.TotalUsado,
                FechaForecast = DateTime.UtcNow
            }).ToList();

            await _historicoRepo.SaveManyAsync(records);

            return historial;
        }

        public async Task<List<ForecastPracticaDto>> ForecastPracticasUsoAsync()
        {
            var solicitudes = await _api.GetSolicitudesUsoAsync();

            var resultado = solicitudes
                .GroupBy(s => new
                {
                    s.PracticaTitulo,
                    Mes = s.FechaSolicitud.ToString("yyyy-MM")
                })
                .Select(g => new ForecastPracticaDto
                {
                    PracticaTitulo = g.Key.PracticaTitulo,
                    Mes = g.Key.Mes,
                    TotalSolicitudes = g.Count(),
                    TotalEstudiantes = g.Sum(x => x.TamanoGrupo * x.NumeroGrupos)
                })
                .OrderByDescending(x => x.Mes)
                .ThenByDescending(x => x.TotalEstudiantes)
                .ToList();

            var records = resultado.Select(p => new ForecastPracticaRecord
            {
                PracticaTitulo = p.PracticaTitulo,
                Mes = p.Mes,
                TotalSolicitudes = p.TotalSolicitudes,
                TotalEstudiantes = p.TotalEstudiantes,
                FechaForecast = DateTime.UtcNow
            }).ToList();

            await _practicaRepo.SaveManyAsync(records);
            return resultado;
        }

        public async Task<List<ForecastRiesgoDto>> ForecastInsumosEnRiesgoAsync()
        {
            var movimientos = await _api.GetMovimientosInventarioAsync();
            var insumos = await _api.GetInsumosAsync();

            // Agrupamos por insumo y mes
            var usoMensual = movimientos
                .Where(m => m.TipoMovimiento.ToUpper() == "PRESTAMO")
                .GroupBy(m => new { m.InsumoNombre, Mes = m.FechaEntregado.ToString("yyyy-MM") })
                .Select(g => new
                {
                    g.Key.InsumoNombre,
                    Total = g.Sum(x => x.Cantidad)
                })
                .GroupBy(x => x.InsumoNombre)
                .ToDictionary(
                    g => g.Key,
                    g => g.Average(x => x.Total)
                );

            var resultado = new List<ForecastRiesgoDto>();

            foreach (var insumo in insumos)
            {
                if (!usoMensual.TryGetValue(insumo.Nombre, out var promedio))
                    promedio = 0;

                var meses = promedio > 0 ? insumo.StockActual / promedio : 9999; // o 0 si prefieres

                string riesgo;
                if (promedio == 0)
                    riesgo = "Sin uso";
                else if (meses < 1)
                    riesgo = "Alerta crítica";
                else if (meses < 2)
                    riesgo = "Riesgo moderado";
                else if (insumo.StockActual < insumo.StockMinimo)
                    riesgo = "Debajo del stock mínimo";
                else
                    riesgo = "Sin riesgo";

                resultado.Add(new ForecastRiesgoDto
                {
                    InsumoNombre = insumo.Nombre,
                    StockActual = insumo.StockActual,
                    StockMinimo = insumo.StockMinimo,
                    UsoMensualPromedio = Math.Round(promedio, 2),
                    MesesSobrantes = Math.Round(meses, 2),
                    Riesgo = riesgo
                });
            }

            var records = resultado.Select(r => new ForecastRiesgoRecord
            {
                InsumoNombre = r.InsumoNombre,
                StockActual = r.StockActual,
                StockMinimo = r.StockMinimo,
                UsoMensualPromedio = r.UsoMensualPromedio,
                MesesSobrantes = r.MesesSobrantes,
                Riesgo = r.Riesgo
            }).ToList();

            await _repo.SaveManyAsync(records);
            return resultado
                .OrderBy(r => r.Riesgo)
                .ThenBy(r => r.MesesSobrantes)
                .ToList();
        }

        /// <summary>
        /// Nuevo método: genera forecast de consumo para cada insumo en los próximos 6 meses,
        /// calcula unidades a comprar y costo estimado, y guarda en Mongo.
        /// </summary>
        public async Task<List<ForecastInsumoSemestreDto>> ForecastInsumosSemestreAsync()
        {
            var movimientos = await _api.GetMovimientosInventarioAsync();
            var insumos = await _api.GetInsumosAsync();

            var resultado = new List<ForecastInsumoSemestreDto>();
            var recordsToSave = new List<ForecastSemestreRecord>();
            var detallesToSave = new List<ForecastSemestreDetalleRecord>();

            foreach (var insumo in insumos)
            {
                var movimientosFiltrados = movimientos
                    .Where(m => m.TipoMovimiento.ToUpper() == "PRESTAMO"
                                && m.InsumoNombre.Equals(insumo.Nombre, StringComparison.OrdinalIgnoreCase))
                    .ToList();

                var historicoDaily = movimientosFiltrados
                    .Select(m => new DailyDemandDto
                    {
                        Date = m.FechaEntregado.Date,
                        Quantity = m.Cantidad
                    })
                    .ToList();

                var forecastPoints = await ForecastAsync(historicoDaily, "monthly");
                var pronosticoSemestre = forecastPoints.Take(6).ToList();

                double sumaDouble = pronosticoSemestre.Sum(p => p.ForecastedQuantity);
                int totalPronosticado = (int)Math.Round(sumaDouble);

                int stockActual = insumo.StockActual;
                int unidadesAComprar = Math.Max(0, totalPronosticado - stockActual);
                decimal costoEstimado = unidadesAComprar * _precioUnitario;

                resultado.Add(new ForecastInsumoSemestreDto
                {
                    InsumoNombre = insumo.Nombre,
                    StockActual = stockActual,
                    TotalPronosticadoSemestre = totalPronosticado,
                    UnidadesAComprar = unidadesAComprar,
                    CostoEstimado = costoEstimado,
                    PronosticoMensual = pronosticoSemestre
                });

                // Guardar en ForecastSemestreRecord (modelo limitado)
                recordsToSave.Add(new ForecastSemestreRecord
                {
                    InsumoNombre = insumo.Nombre,
                    StockActual = stockActual,
                    TotalPronosticadoSemestre = totalPronosticado,
                    UnidadesAComprar = unidadesAComprar,
                    CostoEstimado = costoEstimado
                });

                // Guardar en ForecastSemestreDetalleRecord (colección auxiliar)
                detallesToSave.Add(new ForecastSemestreDetalleRecord
                {
                    InsumoNombre = insumo.Nombre,
                    PronosticoMensual = pronosticoSemestre
                        .Select(p => new PronosticoMensualItem
                        {
                            PeriodStart = p.PeriodStart,
                            ForecastedQuantity = p.ForecastedQuantity
                        }).ToList(),
                    FechaRegistro = DateTime.UtcNow
                });
            }

            await _semestreRepo.SaveManyAsync(recordsToSave);

            // Insertar en la colección auxiliar
            var detallesColl = _semestreRepo.GetRawCollection<ForecastSemestreDetalleRecord>("ForecastSemestreDetalles");
            await detallesColl.InsertManyAsync(detallesToSave);

            return resultado;
        }

    }
}
