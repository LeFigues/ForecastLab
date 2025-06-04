using fl_front.Dtos;
using System.Net.Http.Json;

namespace fl_front.Services.Impl
{
    public class ForecastHistoricoService : IForecastHistoricoService
    {
        private readonly HttpClient _http;

        public ForecastHistoricoService(HttpClient http) => _http = http;

        public async Task<ForecastHistoricoSemestreDto[]> GetHistoricoSemestreAsync()
        {
            // Llama a: GET https://{tu-base-url}/api/forecast/insumos/semestre/historico
            return await _http.GetFromJsonAsync<ForecastHistoricoSemestreDto[]>(
                "api/forecast/insumos/semestre/historico"
            ) ?? new ForecastHistoricoSemestreDto[0];
        }
    }
}
