using fl_api.Dtos.Planification;
using fl_api.Interfaces.Planification;
using fl_api.Interfaces.University;
using fl_api.Models.Forecast;
using fl_api.Models.Planification;

namespace fl_api.Services.Planification
{
    public class PlanningService : IPlanningService
    {
        private readonly IUniversityForecastService _forecastService;
        private readonly IPurchasePlanRepository _repo;
        private readonly IForecastSemestreRepository _repoForecast;

        public PlanningService(
            IUniversityForecastService forecastService,
            IPurchasePlanRepository repo,
            IForecastSemestreRepository repoForecast)
        {
            _forecastService = forecastService;
            _repo = repo;
            _repoForecast = repoForecast;
        }

        public async Task<List<PurchasePlan>> GenerarPlanesDeCompraAsync()
        {
            var forecast = await _repoForecast.GetForecastPendienteAsync(); // desde Mongo directamente

            var planes = forecast
                .Select(f => new PurchasePlan
                {
                    Insumo = f.InsumoNombre,
                    UnidadesAComprar = f.UnidadesAComprar,
                    FechaSugeridaCompra = DateTime.UtcNow.AddDays(10),
                    Estado = "Pendiente"
                }).ToList();

            await _repo.SaveManyAsync(planes);
            return planes;
        }
        public async Task<List<PurchasePlan>> GetPlanesAsync()
        {
            return await _repo.GetAllAsync();
        }
        public async Task<List<CalendarEventDto>> GetCalendarioComprasAsync()
        {
            var planes = await _repo.GetAllAsync();

            return planes.Select(p => new CalendarEventDto
            {
                Title = $"Comprar {p.UnidadesAComprar} de {p.Insumo}",
                Date = p.FechaSugeridaCompra.ToString("yyyy-MM-dd"),
                Color = p.Estado switch
                {
                    "Ordenado" => "green",
                    "Planificado" => "blue",
                    _ => "yellow"
                }
            }).ToList();
        }

    }
}
