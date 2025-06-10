using fl_api.Configurations;
using fl_api.Dtos.University;
using fl_api.Interfaces.University;

namespace fl_api.Services.University
{
    public class UniversityApiConfigService : IUniversityApiConfigService
    {
        private readonly IUniversityApiConfigRepository _repo;
        private readonly HttpClient _httpClient;

        public UniversityApiConfigService(IUniversityApiConfigRepository repo)
        {
            _repo = repo;
            _httpClient = new HttpClient();
        }

        public async Task<UniversityApiConfigDto?> GetConfigAsync()
        {
            var config = await _repo.GetAsync();
            return config is null ? null : new UniversityApiConfigDto
            {
                BaseUrl = config.BaseUrl,
                ApiKey = config.ApiKey
            };
        }

        public async Task SaveConfigAsync(UniversityApiConfigDto dto)
        {
            var model = new UniversityApiConfig
            {
                BaseUrl = dto.BaseUrl.TrimEnd('/'),
                ApiKey = dto.ApiKey
            };

            await _repo.SaveAsync(model);
        }

        public async Task<bool> TestConnectionAsync()
        {
            var config = await _repo.GetAsync();
            if (config is null) return false;

            try
            {
                var request = new HttpRequestMessage(HttpMethod.Get, $"{config.BaseUrl}/semestres");
                if (!string.IsNullOrEmpty(config.ApiKey))
                    request.Headers.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", config.ApiKey);

                var response = await _httpClient.SendAsync(request);
                return response.IsSuccessStatusCode;
            }
            catch
            {
                return false;
            }
        }
    }

}
