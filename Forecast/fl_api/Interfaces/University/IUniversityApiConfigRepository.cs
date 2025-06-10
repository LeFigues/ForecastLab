using fl_api.Configurations;

namespace fl_api.Interfaces.University
{
    public interface IUniversityApiConfigRepository
    {
        Task<UniversityApiConfig?> GetAsync();
        Task SaveAsync(UniversityApiConfig config);
    }
}
