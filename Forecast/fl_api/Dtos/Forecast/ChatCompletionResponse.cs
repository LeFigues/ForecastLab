using DocumentFormat.OpenXml.Drawing.Diagrams;

namespace fl_api.Dtos.Forecast
{
    public class ChatCompletionResponse
    {
        public List<Choice> Choices { get; set; } = new();

        public string GetText()
        {
            return Choices?.FirstOrDefault()?.Message?.Content ?? "";
        }
    }
}
