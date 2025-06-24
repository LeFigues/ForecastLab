namespace fl_api.Models.Guides
{
    public class BulkGuideUploadRequest
    {
        public string Ciclo { get; set; } = string.Empty;          // ej: "2025-2"
        public string Facultad { get; set; } = string.Empty;       // Facultad para el plan de compra
        public List<GuideUploadRequest> Guias { get; set; } = new();
    }
}
