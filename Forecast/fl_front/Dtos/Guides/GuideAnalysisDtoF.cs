namespace fl_front.Dtos.Guides
{
    public class GuideAnalysisDtoF
    {
        public string Id { get; set; } = string.Empty;
        public string GuideFileId { get; set; } = string.Empty;
        public string RegistryCode { get; set; } = string.Empty;
        public string Version { get; set; } = string.Empty;
        public string Subject { get; set; } = string.Empty;
        public string ModelUsed { get; set; } = string.Empty;
        public DateTime CreatedAt { get; set; }
        public string RawText { get; set; } = string.Empty;
        public object RawJson { get; set; } = new();
        public string FileName { get; set; } = string.Empty;
    }
}
