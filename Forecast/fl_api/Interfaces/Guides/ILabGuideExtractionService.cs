using fl_api.Models.Guides;

namespace fl_api.Interfaces.Guides
{
    public interface ILabGuideExtractionService
    {
        Task<LabGuide> AnalyzeGuideFromPdfAsync(string guideFileId, string model = "gpt-4o");
        Task<List<LabGuide>> GetAllAsync();

    }
}
