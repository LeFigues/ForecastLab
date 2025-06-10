using fl_front.Models;
using System.Net.Http.Json;

namespace fl_front.Services.Impl
{
    public class HealthService : IHealthService
    {
        private readonly HttpClient _http;

        public HealthService(HttpClient http)
        {
            _http = http;
        }

        public async Task<HealthSimpleResponse?> GetHealthStatusAsync()
        {
            try
            {
                return await _http.GetFromJsonAsync<HealthSimpleResponse>("api/health");
            }
            catch
            {
                return null;
            }
        }
    }
}
