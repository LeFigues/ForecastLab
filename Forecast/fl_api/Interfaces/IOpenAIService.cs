namespace fl_api.Interfaces
{
    public interface IOpenAIService
    {
        Task<bool> CanConnectAsync();
        Task<string> AnalyzeTextAsync(string prompt, string model);
        Task<string?> AnalyzeWithAssistantAsync(string text, string assistantId);
    }
}
