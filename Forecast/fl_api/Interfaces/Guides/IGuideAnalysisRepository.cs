using fl_api.Models.Guides;

namespace fl_api.Interfaces.Guides
{
    public interface IGuideAnalysisRepository
    {
        Task SaveAsync(GuideAnalysisResult result);
        Task<GuideAnalysisResult?> GetByGuideFileIdAsync(string guideFileId);
    }
}
