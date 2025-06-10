using fl_api.Configurations;
using fl_api.Interfaces;
using Microsoft.Extensions.Options;
using System.Text.Json;
using System.Text;
using System.Net.Http;
using fl_api.Dtos;
using fl_api.Dtos.Forecast;

namespace fl_api.Services
{
    public class OpenAIService : IOpenAIService
    {
        private readonly HttpClient _http;
        private readonly OpenAISettings _settings;

        public OpenAIService(HttpClient http, IOptions<OpenAISettings> options)
        {
            _http = http;
            _settings = options.Value;
        }

        public async Task<string> AnalyzeTextAsync(string text, string model)
        {
            return await AnalyzeWithAssistantAsync(text, _settings.AssistantId!);
        }


        public async Task<bool> CanConnectAsync()
        {
            try
            {
                var result = await AnalyzeTextAsync("Dime hola", "gpt-3.5-turbo");
                return !string.IsNullOrWhiteSpace(result);
            }
            catch
            {
                return false;
            }
        }

        public async Task<string> AnalyzeWithAssistantAsync(string inputText, string assistantId)
        {
            // Paso 1: Crear thread vacío
            var threadResp = await _http.PostAsync("https://api.openai.com/v1/threads", null);
            var threadJson = await threadResp.Content.ReadAsStringAsync();
            if (!threadResp.IsSuccessStatusCode)
                throw new Exception($"Error creando thread: {threadJson}");

            var threadId = JsonDocument.Parse(threadJson).RootElement.GetProperty("id").GetString();

            // Paso 2: Agregar mensaje del usuario
            var messagePayload = new
            {
                role = "user",
                content = inputText
            };

            var msgContent = new StringContent(JsonSerializer.Serialize(messagePayload), Encoding.UTF8, "application/json");
            var msgResp = await _http.PostAsync($"https://api.openai.com/v1/threads/{threadId}/messages", msgContent);
            var msgJson = await msgResp.Content.ReadAsStringAsync();
            if (!msgResp.IsSuccessStatusCode)
                throw new Exception($"Error enviando mensaje al thread: {msgJson}");

            // Paso 3: Crear run
            var runPayload = new
            {
                assistant_id = assistantId
            };

            var runContent = new StringContent(JsonSerializer.Serialize(runPayload), Encoding.UTF8, "application/json");
            var runResp = await _http.PostAsync($"https://api.openai.com/v1/threads/{threadId}/runs", runContent);
            var runJson = await runResp.Content.ReadAsStringAsync();
            if (!runResp.IsSuccessStatusCode)
                throw new Exception($"Error creando run: {runJson}");

            var runId = JsonDocument.Parse(runJson).RootElement.GetProperty("id").GetString();

            // Paso 4: Esperar finalización de la run (polling)
            while (true)
            {
                await Task.Delay(1000);
                var checkRun = await _http.GetAsync($"https://api.openai.com/v1/threads/{threadId}/runs/{runId}");
                var statusJson = await checkRun.Content.ReadAsStringAsync();

                if (!checkRun.IsSuccessStatusCode)
                    throw new Exception($"Error consultando estado del run: {statusJson}");

                var status = JsonDocument.Parse(statusJson).RootElement.GetProperty("status").GetString();
                if (status == "completed") break;
                if (status == "failed" || status == "cancelled" || status == "expired")
                    throw new Exception($"OpenAI run failed with status: {status}");
            }

            // Paso 5: Obtener mensaje generado por el asistente
            var resultResp = await _http.GetAsync($"https://api.openai.com/v1/threads/{threadId}/messages");
            var resultJson = await resultResp.Content.ReadAsStringAsync();
            if (!resultResp.IsSuccessStatusCode)
                throw new Exception($"Error obteniendo mensajes: {resultJson}");

            var messages = JsonDocument.Parse(resultJson).RootElement.GetProperty("data");

            foreach (var message in messages.EnumerateArray())
            {
                if (message.GetProperty("role").GetString() == "assistant")
                {
                    var content = message.GetProperty("content")[0].GetProperty("text").GetProperty("value").GetString();
                    return content ?? throw new Exception("Respuesta vacía del asistente.");
                }
            }

            throw new Exception("No se encontró ningún mensaje del asistente.");
        }

        public async Task<ChatCompletionResponse> CreateChatCompletionAsync(ChatCompletionRequest request)
        {
            // Sólo asignamos el default si NO se pasó explicitamente un model
            request.Model = string.IsNullOrWhiteSpace(request.Model)
                ? _settings.AssistantId
                : request.Model;

            var response = await _http.PostAsJsonAsync("/v1/chat/completions", request);
            response.EnsureSuccessStatusCode();
            var result = await response.Content.ReadFromJsonAsync<ChatCompletionResponse>();
            if (result == null) throw new Exception("OpenAI devolvió un cuerpo vacío.");
            return result;
        }
    }
}
