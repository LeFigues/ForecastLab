using fl_front.Dtos.Guides;
using fl_front.Interfaces;
using Microsoft.AspNetCore.Components.Forms;
using System.Net.Http.Json;

namespace fl_front.Services.Pdfs
{
    public class PdfAnalysisServiceF : IPdfAnalysisServiceF
    {
        private readonly HttpClient _http;

        public PdfAnalysisServiceF(HttpClient http)
        {
            _http = http;
        }

        public async Task<bool> UploadPdfAsync(IBrowserFile file, string tipo)
        {
            var form = new MultipartFormDataContent();
            var stream = file.OpenReadStream(20 * 1024 * 1024);
            form.Add(new StreamContent(stream), "file", file.Name);
            form.Add(new StringContent(tipo), "type");

            var response = await _http.PostAsync("api/pdf/upload", form);
            return response.IsSuccessStatusCode;
        }

        public async Task<string> AnalyzePdfGuideAsync(IBrowserFile file, string modelo)
        {
            var form = new MultipartFormDataContent();
            var stream = file.OpenReadStream(20 * 1024 * 1024);
            form.Add(new StreamContent(stream), "File", file.Name);
            form.Add(new StringContent(modelo), "Model");

            var response = await _http.PostAsync("api/pdf/auto/guide", form);
            return response.IsSuccessStatusCode
                ? await response.Content.ReadAsStringAsync()
                : "Error al procesar el archivo.";
        }

        public async Task<List<GuideFileDtoF>> GetFilesAsync()
        {
            return await _http.GetFromJsonAsync<List<GuideFileDtoF>>("api/pdf/files") ?? new();
        }

        public async Task<GuideAnalysisDtoF?> GetAnalysisByFileIdAsync(string id)
        {
            var response = await _http.GetAsync($"api/pdf/analysis/{id}");
            return response.IsSuccessStatusCode
                ? await response.Content.ReadFromJsonAsync<GuideAnalysisDtoF>()
                : null;
        }
    }
}
