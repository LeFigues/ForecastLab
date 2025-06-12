using fl_front.Dtos.Planification;
using fl_front.Interfaces;
using System.Net.Http.Json;

namespace fl_front.Services.Planification
{
    public class PlanningServiceF : IPlanningServiceF
    {
        private readonly HttpClient _http;

        public PlanningServiceF(HttpClient http)
        {
            _http = http;
        }

        public async Task<List<PlanCompraDtoF>> GetPlanesAsync()
        {
            return await _http.GetFromJsonAsync<List<PlanCompraDtoF>>("api/planning") ?? new();
        }

        public async Task<List<EventoCompraDtoF>> GetCalendarioEventosAsync()
        {
            return await _http.GetFromJsonAsync<List<EventoCompraDtoF>>("api/planning/calendar") ?? new();
        }

        public async Task<bool> GenerarPlanesAsync()
        {
            var response = await _http.PostAsync("api/planning/generar", null);
            return response.IsSuccessStatusCode;
        }
    }
}
