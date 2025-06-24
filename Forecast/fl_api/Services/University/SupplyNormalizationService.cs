using fl_api.Interfaces;
using fl_api.Interfaces.University;
using fl_api.Models.University;
using System.Text.Json;
using System.Text.RegularExpressions;
using University.Interfaces;
using University.Models;

namespace fl_api.Services.University
{
    public class SupplyNormalizationService : ISupplyNormalizationService
    {
        private readonly IUniversityApiClient _universityApi;
        private readonly INormalizedSupplyRepository _repository;
        private readonly IOpenAIService _openAI;

        public SupplyNormalizationService(
            IUniversityApiClient universityApi,
            INormalizedSupplyRepository repository,
            IOpenAIService openAI)
        {
            _universityApi = universityApi;
            _repository = repository;
            _openAI = openAI;
        }

        public async Task<List<NormalizedSupply>> NormalizeAndSyncAsync(bool forceRefresh = false)
        {
            var rawInsumos = await _universityApi.GetInsumosAsync(); // List<Insumo>
            var normalizedList = new List<NormalizedSupply>();

            foreach (var insumo in rawInsumos)
            {
                if (!forceRefresh)
                {
                    var existing = await _repository.GetByIdInsumoAsync(insumo.IdInsumo);
                    if (existing != null)
                    {
                        normalizedList.Add(existing);
                        continue;
                    }
                }

                var nombreNormalizado = NormalizeName(insumo.Nombre);
                // var infoIA = await GetPriceAndLifeFromAI(insumo.Nombre, insumo.Descripcion);
                var infoIA = (precio: 0m, vidaUtil: 12); // sin llamada a IA

                var normalized = new NormalizedSupply
                {
                    IdInsumo = insumo.IdInsumo,
                    Nombre = insumo.Nombre,
                    NombreNormalizado = nombreNormalizado,
                    Descripcion = insumo.Descripcion,
                    Tipo = insumo.Tipo,
                    UnidadMedida = insumo.UnidadMedida,
                    StockTotal = insumo.StockActual,
                    StockMinimo = insumo.StockMinimo,
                    StockMaximo = insumo.StockMaximo,
                    PrecioEstimado = infoIA.precio,
                    VidaUtilMeses = infoIA.vidaUtil,
                    AñoCompra = 2023,
                    PrecioGeneradoPorIA = false,
                    VidaUtilGeneradaPorIA = false
                };

                await _repository.UpsertAsync(normalized);
                normalizedList.Add(normalized);
            }

            return normalizedList;
        }


        public async Task<NormalizedSupply?> NormalizeSingleAsync(int idInsumo)
        {
            var rawInsumos = await _universityApi.GetInsumosAsync();
            var insumo = rawInsumos.FirstOrDefault(x => x.IdInsumo == idInsumo);
            if (insumo == null) return null;

            var nombreNormalizado = NormalizeName(insumo.Nombre);
            // var infoIA = await GetPriceAndLifeFromAI(insumo.Nombre, insumo.Descripcion);
            var infoIA = (precio: 0m, vidaUtil: 12); // sin nullable

            var normalized = new NormalizedSupply
            {
                IdInsumo = insumo.IdInsumo,
                Nombre = insumo.Nombre,
                NombreNormalizado = nombreNormalizado,
                Descripcion = insumo.Descripcion,
                Tipo = insumo.Tipo,
                UnidadMedida = insumo.UnidadMedida,
                StockTotal = insumo.StockActual,
                StockMinimo = insumo.StockMinimo,
                StockMaximo = insumo.StockMaximo,
                PrecioEstimado = infoIA.precio,
                VidaUtilMeses = infoIA.vidaUtil,
                AñoCompra = 2023,
                PrecioGeneradoPorIA = false,
                VidaUtilGeneradaPorIA = false
            };

            await _repository.UpsertAsync(normalized);
            return normalized;
        }


        public async Task<bool> NeedsNormalizationAsync(int idInsumo)
        {
            var existing = await _repository.GetByIdInsumoAsync(idInsumo);
            return existing == null;
        }

        private string NormalizeName(string name)
        {
            var normalized = name.ToLowerInvariant().Trim();
            normalized = Regex.Replace(normalized, @"[^\w\s]", "");
            normalized = Regex.Replace(normalized, @"\s+", " ");
            return normalized;
        }

        private async Task<(decimal precio, int vidaUtil)?> GetPriceAndLifeFromAI(string nombre, string descripcion)
        {
            var prompt = $"Dame el precio estimado en bolivianos y el tiempo de vida útil en meses para este insumo de laboratorio:\n\n" +
                         $"Nombre: {nombre}\nDescripción: {descripcion}\n\n" +
                         $"Devuélvelo como JSON así:\n{{ \"precio\": 12.50, \"vidaUtil\": 18 }}";

            try
            {
                var result = await _openAI.AnalyzeTextAsync(prompt, "gpt-4");
                var json = JsonSerializer.Deserialize<JsonElement>(result);

                var precio = json.GetProperty("precio").GetDecimal();
                var vidaUtil = json.GetProperty("vidaUtil").GetInt32();
                return (precio, vidaUtil);
            }
            catch
            {
                return null;
            }
        }
    }
}
