namespace fl_api.Models.Guides
{
    public class GuideUploadRequest
    {
        public IFormFile File { get; set; } = null!;
        public string Carrera { get; set; } = string.Empty;
        public string Materia { get; set; } = string.Empty;
    }
}
