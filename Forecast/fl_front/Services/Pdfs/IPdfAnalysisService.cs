using fl_front.Models.Guides;

namespace fl_front.Services.Pdfs
{
    public interface IPdfAnalysisService
    {
        Task<PdfPracticeAnalysisResult?> AnalyzePdfAsync(Stream fileStream, string fileName);
    }
}
