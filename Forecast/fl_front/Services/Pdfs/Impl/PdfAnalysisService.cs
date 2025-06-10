using fl_front.Models.Guides;
using System.Text.Json;

namespace fl_front.Services.Pdfs.Impl
{
    public class PdfAnalysisService : IPdfAnalysisService
    {
        private readonly HttpClient _http;

        public PdfAnalysisService(HttpClient http)
        {
            _http = http;
        }

        public async Task<PdfPracticeAnalysisResult?> AnalyzePdfAsync(Stream fileStream, string fileName)
        {
            var content = new MultipartFormDataContent
        {
            { new StreamContent(fileStream), "file", fileName },
            { new StringContent("practica"), "type" },
            { new StringContent("gpt-4o"), "model" }
        };

            var response = await _http.PostAsync("/api/pdf/auto", content);
            if (!response.IsSuccessStatusCode) return null;

            var jsonDoc = JsonDocument.Parse(await response.Content.ReadAsStringAsync());
            if (jsonDoc.RootElement.TryGetProperty("rawJson", out var rawJson))
            {
                return JsonSerializer.Deserialize<PdfPracticeAnalysisResult>(rawJson.GetRawText(),
                    new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
            }

            return null;
        }
    }

}
