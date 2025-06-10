using fl_api.Interfaces.Guides;
using fl_api.Interfaces;
using fl_api.Models.Guides;
using MongoDB.Bson;

namespace fl_api.Services.Guides
{
    public class GuideGroupAnalysisService : IGuideGroupAnalysisService
    {
        private readonly IOpenAIService _openAi;
        private readonly IGuideAnalysisRepository _repo;

        public GuideGroupAnalysisService(IOpenAIService openAi, IGuideAnalysisRepository repo)
        {
            _openAi = openAi;
            _repo = repo;
        }

        public async Task<GuideAnalysisResult> AnalyzeGuideAsync(string guideFileId, string rawText, string model)
        {
            var assistantResponse = await _openAi.AnalyzeWithAssistantAsync(rawText, "asst_id_guide_group");

            var doc = BsonDocument.Parse(assistantResponse);
            if (doc.Contains("grupos") && doc["grupos"].IsString)
                doc["grupos"] = int.TryParse(doc["grupos"].AsString, out int g) ? g : 0;

            if (doc.Contains("estudiantes_por_grupo") && doc["estudiantes_por_grupo"].IsString)
                doc["estudiantes_por_grupo"] = int.TryParse(doc["estudiantes_por_grupo"].AsString, out int e) ? e : 0;

            var result = new GuideAnalysisResult
            {
                GuideFileId = ObjectId.Parse(guideFileId),
                ModelUsed = model,
                CreatedAt = DateTime.UtcNow,
                RawText = assistantResponse,
                RawJson = doc,
                Type = "guia",
                RegistryCode = doc.GetValue("codigo_registro", "Desconocido").AsString,
                Version = doc.GetValue("version", "Desconocido").AsString,
                Subject = doc.GetValue("materia", "Desconocido").AsString,
                Faculty = doc.GetValue("facultad", "Desconocido").AsString,
                Career = doc.GetValue("carrera", "Desconocido").AsString
            };

            if (doc.Contains("practicas") && doc["practicas"].IsBsonArray)
            {
                var items = new List<PracticeItem>();
                foreach (var pr in doc["practicas"].AsBsonArray)
                {
                    var p = pr.AsBsonDocument;
                    var item = new PracticeItem
                    {
                        PracticeNumber = p.GetValue("practica_numero", 0).ToInt32(),
                        PracticeTitle = p.GetValue("titulo", "Desconocido").AsString,
                        GroupCount = doc.GetValue("grupos", 0).ToInt32(),
                        StudentsPerGroup = doc.GetValue("estudiantes_por_grupo", 0).ToInt32(),
                        Equipment = ParseMaterialList(p, "equipos"),
                        Supplies = ParseMaterialList(p, "insumos"),
                        Reagents = ParseMaterialList(p, "reactivos")
                    };
                    items.Add(item);
                }
                result.Practices = items;
            }

            await _repo.SaveAsync(result);
            return result;
        }

        private List<MaterialItem> ParseMaterialList(BsonDocument doc, string field)
        {
            var list = new List<MaterialItem>();
            if (doc.Contains("materiales") && doc["materiales"].IsBsonDocument)
            {
                var mat = doc["materiales"].AsBsonDocument;
                if (mat.Contains(field) && mat[field].IsBsonArray)
                {
                    foreach (var el in mat[field].AsBsonArray)
                    {
                        var m = el.AsBsonDocument;
                        list.Add(new MaterialItem
                        {
                            Description = m.GetValue("descripcion", "Desconocido").AsString,
                            Unit = m.GetValue("unidad", "Desconocido").AsString,
                            Quantity = m.GetValue("cantidad_por_grupo", 0).ToInt32()
                        });
                    }
                }
            }
            return list;
        }
    }
}
