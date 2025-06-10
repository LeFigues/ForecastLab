namespace fl_api.Interfaces.IForecast
{
    public interface IPromptService
    {
        /// <summary>
        /// Devuelve el texto del prompt identificado por 'key'.
        /// </summary>
        string GetPrompt(string key);
    }
}
