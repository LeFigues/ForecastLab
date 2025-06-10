using fl_front.Models;
using System.Net.Http.Json;

namespace fl_front.Services.Impl
{
    public class UniversidadDemandService : IUniversidadDemandService
    {
        private readonly HttpClient _http;

        public UniversidadDemandService(HttpClient http)
        {
            _http = http;
        }

        public async Task<List<Insumo>> GetInsumosAsync()
        {
            try
            {
                var insumos = await _http.GetFromJsonAsync<List<Insumo>>("https://universidad-la9h.onrender.com/insumos");
                return insumos ?? new List<Insumo>();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error al obtener insumos: {ex.Message}");
                return new List<Insumo>();
            }
        }
    }
}
