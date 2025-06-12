using fl_front.Dtos.Guides;
using Microsoft.AspNetCore.Components.Forms;

namespace fl_front.Interfaces
{
    public interface IPdfAnalysisServiceF
    {
        Task<bool> UploadPdfAsync(IBrowserFile file, string tipo);
        Task<string> AnalyzePdfGuideAsync(IBrowserFile file, string modelo);
        Task<List<GuideFileDtoF>> GetFilesAsync();
        Task<GuideAnalysisDtoF?> GetAnalysisByFileIdAsync(string id);
    }
}
