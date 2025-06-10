namespace fl_api.Configurations
{
    public class PromptSettingsA
    {
        public PromptSection Correction { get; set; } = new();
        public PromptSection Prediction { get; set; } = new();
        public PromptSection Structuring { get; set; } = new();
    }
}
