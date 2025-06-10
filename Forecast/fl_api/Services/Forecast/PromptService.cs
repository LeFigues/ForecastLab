using fl_api.Configurations;
using fl_api.Interfaces.IForecast;
using Microsoft.Extensions.Options;

namespace fl_api.Services.Forecast
{
    public class PromptService : IPromptService
    {
        private readonly PromptSettingsA _settings;
        public PromptService(IOptions<PromptSettingsA> opts)
            => _settings = opts.Value;

        // Implementamos exactamente el método de la interfaz
        public string GetPrompt(string key) => key switch
        {
            "correction" => _settings.Correction.User,
            "structuring" => _settings.Structuring.User,
            "prediction" => _settings.Prediction.User,
            _ => throw new KeyNotFoundException($"Prompt '{key}' not found")
        };

    }
}
