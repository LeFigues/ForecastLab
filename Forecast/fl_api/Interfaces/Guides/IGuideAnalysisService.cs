using fl_api.Models.Guides;

namespace fl_api.Interfaces.Guides
{
    public interface IGuideAnalysisService
    {
        Task<GuideAnalysisResult> AnalyzeAsync(string guideFileId, string model, string type);
        Task<GuideAnalysisResult> AnalyzeRawTextAsync(string guideFileId, string rawText, string model, string type);
    }
}
