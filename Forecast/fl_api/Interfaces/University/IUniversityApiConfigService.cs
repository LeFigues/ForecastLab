using fl_api.Dtos.University;

namespace fl_api.Interfaces.University
{
    public interface IUniversityApiConfigService
    {
        Task<UniversityApiConfigDto?> GetConfigAsync();
        Task SaveConfigAsync(UniversityApiConfigDto dto);
        Task<bool> TestConnectionAsync();
    }
}
