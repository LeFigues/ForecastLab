namespace fl_api.Dtos.Guides
{
    public class GuideAnalysisRequest
    {
        public string Model { get; set; } = "gpt-3.5-turbo";
        public string Type { get; set; } = null!; // "guia" o "practica"
    }
}
