namespace fl_api.Dtos.Guides
{
    public class GuideGroupAnalysisRequest
    {
        public IFormFile File { get; set; } = null!;
        public string Model { get; set; } = "gpt-4o";
    }
}
