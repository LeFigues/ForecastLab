using fl_api.Models.Guides;

namespace fl_api.Interfaces.Guides
{
    public interface ILabGuideRepository
    {
        Task SaveAsync(LabGuide guide);
        Task<List<LabGuide>> GetAllAsync();
        Task<LabGuide?> GetByIdAsync(string id);
        Task<LabGuide?> GetByGuideFileIdAsync(string guideFileId);
        Task<List<LabGuide>> GetByCycleAndFacultyAsync(string ciclo, string facultad);

    }
}
