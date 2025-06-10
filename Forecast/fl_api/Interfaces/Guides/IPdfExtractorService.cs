namespace fl_api.Interfaces.Guides
{
    public interface IPdfExtractorService
    {
        Task<string> ExtractTextAsync(IFormFile pdfFile);
    }
}
