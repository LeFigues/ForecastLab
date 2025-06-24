using fl_api.Models.Guides;

namespace fl_api.Interfaces.Guides
{
    public interface IGuideUploadService
    {
        Task<GuideFileRecord> SaveGuideAsync(IFormFile file, string ciclo, string facultad, string carrera, string materia);
        Task<List<GuideFileRecord>> GetAllAsync();

    }
}
