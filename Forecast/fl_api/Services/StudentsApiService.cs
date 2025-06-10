using fl_api.Interfaces;

namespace fl_api.Services
{
    public class StudentsApiService : IStudentsApiService
    {
        private readonly HttpClient _http;
        public StudentsApiService(HttpClient http) => _http = http;

        public async Task<bool> CanConnectAsync()
        {
            try
            {
                // Llamamos sin parámetros y comprobamos status
                using var resp = await _http.GetAsync("api/health");
                return resp.IsSuccessStatusCode;
            }
            catch
            {
                return false;
            }
        }
    }
}
