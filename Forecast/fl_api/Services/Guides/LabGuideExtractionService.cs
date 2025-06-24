using fl_api.Configurations;
using fl_api.Interfaces;
using fl_api.Interfaces.Guides;
using fl_api.Models.Forecast;
using fl_api.Models.Guides;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;

namespace fl_api.Services.Forecast
{
    public class LabGuideExtractionService : ILabGuideExtractionService
    {
        private readonly IGuideFileRepository _fileRepo;
        private readonly IPdfExtractorService _pdfExtractor;
        private readonly IOpenAIService _openAi;
        private readonly string _assistantId;
        private readonly ILabGuideRepository _labGuideRepository;

        public LabGuideExtractionService(
            IGuideFileRepository fileRepo,
            IPdfExtractorService pdfExtractor,
            IOpenAIService openAi,
            IOptions<OpenAISettings> openAiOptions,
            ILabGuideRepository labGuideRepository)
        {
            _fileRepo = fileRepo;
            _pdfExtractor = pdfExtractor;
            _openAi = openAi;
            _assistantId = openAiOptions.Value.AssistantId;
            _labGuideRepository = labGuideRepository;
        }

        public async Task<List<LabGuide>> GetAllAsync()
        {
            return await _labGuideRepository.GetAllAsync();
        }

        public async Task<LabGuide> AnalyzeGuideFromPdfAsync(string guideFileId, string model = "gpt-4o")
        {
            // 1. Obtener el archivo PDF
            var file = await _fileRepo.GetByIdAsync(guideFileId);
            if (file == null || !File.Exists(file.FilePath))
                throw new FileNotFoundException("PDF file not found");

            await using var stream = File.OpenRead(file.FilePath);
            var formFile = new FormFile(stream, 0, stream.Length, "file", Path.GetFileName(file.FilePath));
            var text = await _pdfExtractor.ExtractTextAsync(formFile);

            if (string.IsNullOrWhiteSpace(text) || text.Length < 100)
                throw new Exception("No valid text extracted from PDF");

            if (text.Length > 10000)
                text = text[..10000];

            // 2. Construir prompt
            var promptTemplate = @"
A partir del siguiente texto extraído de una guía de laboratorio, genera un JSON con la siguiente estructura:
{{
  ""laboratorio"": ""..."",
  ""codigo"": ""..."",
  ""version"": ""..."",
  ""practicas"": [
    {{
      ""titulo"": ""..."",
      ""cantidad_grupos"": ...,
      ""estudiantes_por_grupo"": ...,
      ""materiales"": {{
        ""equipos"": [
          {{ ""cantidad_por_grupo"": ..., ""unidad"": ""..."", ""descripcion"": ""..."" }}
        ],
        ""insumos"": [
          {{ ""cantidad_por_grupo"": ..., ""unidad"": ""..."", ""descripcion"": ""..."" }}
        ]
      }}
    }}
  ]
}}
Solo responde con el JSON válido.
===
{0}
===";

            var prompt = string.Format(promptTemplate, text);

            // 3. Enviar a OpenAI
            var responseJson = await _openAi.AnalyzeWithAssistantAsync(prompt, _assistantId);
            if (string.IsNullOrWhiteSpace(responseJson))
                throw new Exception("OpenAI response is empty.");

            // 4. Limpiar delimitadores de Markdown
            responseJson = responseJson.Trim();
            if (responseJson.StartsWith("```json")) responseJson = responseJson[7..].Trim();
            else if (responseJson.StartsWith("```")) responseJson = responseJson[3..].Trim();
            if (responseJson.EndsWith("```")) responseJson = responseJson[..^3].Trim();
            Console.WriteLine("🔵 OpenAI JSON:");
            Console.WriteLine(responseJson);

            // 5. Deserializar
            LabGuide? labGuide;
            try
            {
                labGuide = JsonConvert.DeserializeObject<LabGuide>(responseJson);
            }
            catch (Exception ex)
            {
                throw new Exception("Failed to parse OpenAI response into LabGuide object.", ex);
            }

            if (labGuide == null)
                throw new Exception("OpenAI JSON is null or couldn't be parsed.");

            if (string.IsNullOrWhiteSpace(labGuide.LabName))
                throw new Exception($"Missing required field 'laboratorio'. JSON: {responseJson}");

            // ✅ 6. Asignar campos extra desde el archivo PDF
            labGuide.GuideFileId = file.Id.ToString();
            labGuide.Cycle = file.Cycle ?? "";
            labGuide.Faculty = file.Faculty ?? "";
            labGuide.Career = file.Career ?? "";
            labGuide.Subject = file.Subject ?? "";

            // ✅ 7. Guardar en MongoDB
            await _labGuideRepository.SaveAsync(labGuide);
            return labGuide;
        }

    }
}
