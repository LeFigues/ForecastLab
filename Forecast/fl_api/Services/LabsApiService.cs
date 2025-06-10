using fl_api.Interfaces;

namespace fl_api.Services
{
    public class LabsApiService : ILabsApiService
    {
        private readonly HttpClient _http;
        public LabsApiService(HttpClient http) => _http = http;

        public async Task<bool> CanConnectAsync()
        {
            try
            {
                using var resp = await _http.GetAsync("tipo/estudiantes");
                return resp.IsSuccessStatusCode;
            }
            catch
            {
                return false;
            }
        }
    }
}
