using fl_front.Dtos.UniversityForecast;
using fl_front.Interfaces;
using System.Net.Http.Json;

namespace fl_front.Services.UniversityForecast
{
    public class UniversityForecastServiceF : IUniversityForecastServiceF
    {
        private readonly HttpClient _http;

        public UniversityForecastServiceF(HttpClient http)
        {
            _http = http;
        }


        public async Task<List<MovimientoResumenDtoF>> GetResumenPorSemestreAsync(string semestreId)
        {
            return await _http.GetFromJsonAsync<List<MovimientoResumenDtoF>>($"api/universityforecast/movimientos-por-semestre/{semestreId}") ?? new();
        }

        public async Task<List<MovimientoDetalleDtoF>> GetDetallePorInsumoAsync(string insumo)
        {
            return await _http.GetFromJsonAsync<List<MovimientoDetalleDtoF>>($"api/universityforecast/movimientos-detalle/{insumo}") ?? new();
        }
    }
}
