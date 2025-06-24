namespace fl_api.Dtos.Guides
{
    public class GuideUploadRequest
    {
        public IFormFile File { get; set; } = null!;
        public string Career { get; set; } = string.Empty;
        public string Subject { get; set; } = string.Empty;
    }
}
