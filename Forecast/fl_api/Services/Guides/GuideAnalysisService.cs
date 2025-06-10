using fl_api.Interfaces.Guides;
using fl_api.Interfaces;
using fl_api.Models.Guides;
using MongoDB.Bson;
using System.Diagnostics;
using fl_api.Configurations;
using Microsoft.Extensions.Options;

namespace fl_api.Services.Guides;

public class GuideAnalysisService : IGuideAnalysisService
{
    private readonly IGuideFileRepository _fileRepo;
    private readonly IGuideAnalysisRepository _analysisRepo;
    private readonly IOpenAIService _openAi;
    private readonly string _assistantId;
    private readonly IPdfExtractorService _pdfExtractor;

    public GuideAnalysisService(
        IGuideFileRepository fileRepo,
        IGuideAnalysisRepository analysisRepo,
        IOpenAIService openAi,
        IOptions<OpenAISettings> openAiOptions,
        IPdfExtractorService pdfExtractor)
    {
        _fileRepo = fileRepo;
        _analysisRepo = analysisRepo;
        _openAi = openAi;
        _assistantId = openAiOptions.Value.AssistantId;
        _pdfExtractor = pdfExtractor;
    }
    public async Task<GuideAnalysisResult> AnalyzeAsync(string guideFileId, string model, string type)
    {
        var file = await _fileRepo.GetByIdAsync(guideFileId);
        if (file == null || !System.IO.File.Exists(file.FilePath))
            throw new Exception("PDF not found");

        await using var stream = System.IO.File.OpenRead(file.FilePath);
        var formFile = new FormFile(stream, 0, stream.Length, "file", Path.GetFileName(file.FilePath));
        var text = await _pdfExtractor.ExtractTextAsync(formFile);

        if (string.IsNullOrWhiteSpace(text) || text.Length < 100)
            throw new Exception("No valid text extracted from PDF");

        if (text.Length > 10000)
            text = text.Substring(0, 10000);

        return await AnalyzeRawTextAsync(guideFileId, text, model, type);
    }

    public async Task<GuideAnalysisResult> AnalyzeRawTextAsync(string guideFileId, string rawText, string model, string type)
    {
        if (string.IsNullOrWhiteSpace(rawText) || rawText.Length < 100)
            throw new Exception("No valid text provided");

        if (rawText.Length > 10000)
            rawText = rawText.Substring(0, 10000);

        var jsonText = await _openAi.AnalyzeWithAssistantAsync(rawText, _assistantId);
        if (string.IsNullOrWhiteSpace(jsonText))
            throw new Exception("Empty response from OpenAI");

        var rawJson = TryDeserializeJson(jsonText);

        // Deserialización manual a modelos tipados
        string GetString(BsonDocument doc, string key, string fallback = "Desconocido") => doc.Contains(key) ? doc[key].ToString() : fallback;
        int GetInt(BsonDocument doc, string key) => doc.Contains(key) && int.TryParse(doc[key].ToString(), out var val) ? val : 0;

        var titulo = GetString(rawJson, "titulo");
        var grupos = GetInt(rawJson, "grupos");

        var equipos = new List<MaterialItem>();
        var insumos = new List<MaterialItem>();

        if (rawJson.Contains("materiales") && rawJson["materiales"].IsBsonDocument)
        {
            var materiales = rawJson["materiales"].AsBsonDocument;

            if (materiales.Contains("equipos") && materiales["equipos"].IsBsonArray)
            {
                foreach (var eq in materiales["equipos"].AsBsonArray)
                {
                    equipos.Add(new MaterialItem
                    {
                        Description = eq.AsBsonDocument.GetValue("descripcion", "Desconocido").ToString(),
                        Unit = eq.AsBsonDocument.GetValue("unidad", "Desconocido").ToString(),
                        Quantity = eq.AsBsonDocument.GetValue("cantidad_por_grupo", 0).ToInt32()
                    });
                }
            }

            if (materiales.Contains("insumos") && materiales["insumos"].IsBsonArray)
            {
                foreach (var ins in materiales["insumos"].AsBsonArray)
                {
                    insumos.Add(new MaterialItem
                    {
                        Description = ins.AsBsonDocument.GetValue("descripcion", "Desconocido").ToString(),
                        Unit = ins.AsBsonDocument.GetValue("unidad", "Desconocido").ToString(),
                        Quantity = ins.AsBsonDocument.GetValue("cantidad_por_grupo", 0).ToInt32()
                    });
                }
            }
        }

        var practice = new PracticeItem
        {
            PracticeTitle = titulo,
            GroupCount = grupos,
            Equipment = equipos,
            Supplies = insumos
        };

        var result = new GuideAnalysisResult
        {
            GuideFileId = ObjectId.Parse(guideFileId),
            Type = type,
            ModelUsed = model,
            CreatedAt = DateTime.UtcNow,
            RawText = jsonText,
            RawJson = rawJson,
            Faculty = GetString(rawJson, "facultad"),
            Career = GetString(rawJson, "carrera"),
            Subject = GetString(rawJson, "materia"),
            RegistryCode = GetString(rawJson, "codigo_registro"),
            Version = GetString(rawJson, "version"),
            Practices = new List<PracticeItem> { practice }
        };

        await _analysisRepo.SaveAsync(result);
        return result;
    }

    private BsonDocument TryDeserializeJson(string text)
    {
        try
        {
            Console.WriteLine("📥 Respuesta cruda de OpenAI:");
            Console.WriteLine(text);

            text = text.Trim();
            if (text.StartsWith("```json")) text = text[7..].Trim();
            else if (text.StartsWith("```")) text = text[3..].Trim();
            if (text.EndsWith("```")) text = text[..^3].Trim();

            var doc = MongoDB.Bson.Serialization.BsonSerializer.Deserialize<BsonDocument>(text);

            // Corrige campo grupos (de string a int)
            if (doc.Contains("grupos") && doc["grupos"].IsString)
            {
                if (int.TryParse(doc["grupos"].AsString, out int grupos))
                    doc["grupos"] = grupos;
                else
                    doc["grupos"] = 0;
            }

            // Corrige campo estudiantes_por_grupo (de string a int)
            if (doc.Contains("estudiantes_por_grupo") && doc["estudiantes_por_grupo"].IsString)
            {
                if (int.TryParse(doc["estudiantes_por_grupo"].AsString, out int estu))
                    doc["estudiantes_por_grupo"] = estu;
                else
                    doc["estudiantes_por_grupo"] = 0;
            }

            // Corrige materiales si viene como string JSON
            if (doc.Contains("materiales") && doc["materiales"].IsString)
            {
                try
                {
                    var matDoc = MongoDB.Bson.Serialization.BsonSerializer.Deserialize<BsonDocument>(doc["materiales"].AsString);
                    doc["materiales"] = matDoc;
                }
                catch
                {
                    doc["materiales"] = new BsonDocument
                {
                    { "equipos", new BsonArray() },
                    { "insumos", new BsonArray() },
                    { "reactivos", new BsonArray() }
                };
                }
            }

            // Corrige arrays embebidos como string
            if (doc.Contains("materiales") && doc["materiales"].IsBsonDocument)
            {
                var materiales = doc["materiales"].AsBsonDocument;
                string[] arrayFields = { "equipos", "insumos", "reactivos", "equipment", "supplies", "reagents" };
                foreach (var field in arrayFields)
                {
                    if (materiales.Contains(field))
                    {
                        var f = materiales[field];
                        if (f.IsString)
                        {
                            try
                            {
                                var arr = MongoDB.Bson.Serialization.BsonSerializer.Deserialize<BsonArray>(f.AsString);
                                materiales[field] = arr;
                            }
                            catch
                            {
                                materiales[field] = new BsonArray();
                            }
                        }
                    }
                }
            }

            return doc;
        }
        catch (Exception ex)
        {
            Console.WriteLine($"❌ Error parsing OpenAI response: {ex.Message}");
            return new BsonDocument
        {
            { "error", "Failed to parse response" },
            { "raw", text }
        };
        }
    }
}