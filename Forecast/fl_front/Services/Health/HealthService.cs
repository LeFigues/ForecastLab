using fl_front.Dtos.Health;
using fl_front.Interfaces;
using System.Net.Http.Json;

namespace fl_front.Services.Health
{
    public class HealthService : IHealthService
    {
        private readonly HttpClient _http;

        public HealthService(HttpClient http)
        {
            _http = http;
        }

        public async Task<HealthStatusDto?> GetHealthStatusAsync()
        {
            return await _http.GetFromJsonAsync<HealthStatusDto>("api/health");
        }
    }
}
