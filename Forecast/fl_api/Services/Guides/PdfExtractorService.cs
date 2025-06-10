using fl_api.Interfaces.Guides;
using System.Text;
using UglyToad.PdfPig;
using UglyToad.PdfPig.Content;

namespace fl_api.Services.Guides
{
    public class PdfExtractorService : IPdfExtractorService
    {
        public async Task<string> ExtractTextAsync(IFormFile pdfFile)
        {
            using var stream = pdfFile.OpenReadStream();
            using var ms = new MemoryStream();
            await stream.CopyToAsync(ms);
            ms.Position = 0;

            using var document = PdfDocument.Open(ms);
            var sb = new StringBuilder();

            foreach (Page page in document.GetPages())
            {
                sb.AppendLine(page.Text);
            }

            return sb.ToString();
        }
    }
}
