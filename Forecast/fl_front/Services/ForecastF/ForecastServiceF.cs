using fl_front.Dtos.ForecastF;
using fl_front.Interfaces;
using System.Net.Http.Json;

namespace fl_front.Services.ForecastF
{
    public class ForecastServiceF : IForecastServiceF
    {
        private readonly HttpClient _http;

        public ForecastServiceF(HttpClient http)
        {
            _http = http;
        }

        public Task<List<InsumoPorPracticaDtoF>> GetInsumosPorPracticaAsync() =>
            _http.GetFromJsonAsync<List<InsumoPorPracticaDtoF>>("api/forecast/insumos-por-practica");

        public Task<List<PracticaUsoDtoF>> GetPracticasUsoAsync() =>
            _http.GetFromJsonAsync<List<PracticaUsoDtoF>>("api/forecast/practicas-uso");

        public Task<List<RiesgoInsumoDtoF>> GetRiesgoInsumosAsync() =>
            _http.GetFromJsonAsync<List<RiesgoInsumoDtoF>>("api/forecast/insumos-riesgo");

        public async Task<string> AnalizarConIAAsync(List<RiesgoInsumoDtoF> datos)
        {
            var payload = new
            {
                datos = datos.Select(r => new
                {
                    insumoNombre = r.InsumoNombre,
                    stockActual = r.StockActual,
                    usoMensualPromedio = r.UsoMensualPromedio,
                    mesesSobrantes = r.MesesSobrantes,
                    riesgo = r.Riesgo
                })
            };

            var response = await _http.PostAsJsonAsync("api/forecast/insumos-riesgo/ai", payload);
            return response.IsSuccessStatusCode ? await response.Content.ReadAsStringAsync() : string.Empty;
        }

        public async Task<List<ForecastPointDtoF>> GetPronosticoPorFechasAsync(DateTime from, DateTime to, string horizon)
        {
            var payload = new
            {
                from = from.ToString("yyyy-MM-dd"),
                to = to.ToString("yyyy-MM-dd"),
                horizon
            };
            var response = await _http.PostAsJsonAsync("api/forecast", payload);
            return await response.Content.ReadFromJsonAsync<List<ForecastPointDtoF>>() ?? new();
        }

        public Task<List<HistoricoInsumoDtoF>> GetHistoricoInsumosAsync() =>
            _http.GetFromJsonAsync<List<HistoricoInsumoDtoF>>("api/forecast/insumos-historico/historico");

        public Task<List<HistoricoPracticaDtoF>> GetHistoricoPracticasAsync() =>
            _http.GetFromJsonAsync<List<HistoricoPracticaDtoF>>("api/forecast/practicas-uso/historico");

        public Task<List<HistoricoSemestreDtoF>> GetHistoricoSemestreAsync() =>
            _http.GetFromJsonAsync<List<HistoricoSemestreDtoF>>("api/forecast/insumos/semestre/historico");

        public async Task<List<RiesgoInsumoDtoF>> GetInsumosEnRiesgoAsync()
        {
            return await _http.GetFromJsonAsync<List<RiesgoInsumoDtoF>>("api/forecast/insumos-riesgo");
        }

        public async Task<List<ForecastPointDtoF>> GenerarPronosticoAsync(ForecastRequestDtoF request)
        {
            var response = await _http.PostAsJsonAsync("api/forecast", request);
            if (response.IsSuccessStatusCode)
            {
                return await response.Content.ReadFromJsonAsync<List<ForecastPointDtoF>>();
            }
            return new();
        }

    }
}
