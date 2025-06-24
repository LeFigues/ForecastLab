using fl_api.Models.Guides;

namespace fl_api.Interfaces.Guides
{
    public interface ISemesterGuideAnalysisService
    {
        Task<List<LabGuide>> AnalyzeAllUnprocessedAsync(string ciclo, string facultad);
    }
}
