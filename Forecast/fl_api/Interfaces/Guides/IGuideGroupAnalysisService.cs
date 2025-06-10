using fl_api.Models.Guides;

namespace fl_api.Interfaces.Guides
{
    public interface IGuideGroupAnalysisService
    {
        Task<GuideAnalysisResult> AnalyzeGuideAsync(string guideFileId, string rawText, string model);
    }
}
