using System.Text.Json.Serialization;

namespace fl_front.Models.Guides
{
    public class PdfPracticeAnalysisResult
    {
        public string? Laboratorio { get; set; }
        public string? Titulo { get; set; }
        public int Grupos { get; set; }

        [JsonPropertyName("estudiantes_por_grupo")]
        public int EstudiantesPorGrupo { get; set; }

        public List<PracticaItem> Practicas { get; set; } = new();
    }
}
