using fl_api.Models.Guides;

namespace fl_api.Interfaces.Guides
{
    public interface IGuideFileRepository
    {
        Task SaveAsync(GuideFileRecord file);
        Task<GuideFileRecord?> GetByIdAsync(string id);
        Task<List<GuideFileRecord>> GetAllAsync();
    }
}
