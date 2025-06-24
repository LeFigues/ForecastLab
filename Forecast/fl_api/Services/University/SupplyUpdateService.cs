using fl_api.Interfaces.University;
using fl_api.Interfaces;
using fl_api.Models.University;
using System.Text.Json;

namespace fl_api.Services.University
{
    public class SupplyUpdateService : ISupplyUpdateService
    {
        private readonly INormalizedSupplyRepository _repository;
        private readonly IOpenAIService _openAI;

        public SupplyUpdateService(INormalizedSupplyRepository repository, IOpenAIService openAI)
        {
            _repository = repository;
            _openAI = openAI;
        }

        public async Task<List<NormalizedSupply>> UpdateAllAsync()
        {
            var all = await _repository.GetAllAsync();
            return await UpdateBatchAsync(all);
        }

        public async Task<List<NormalizedSupply>> UpdateByIdsAsync(List<int> ids)
        {
            var all = await _repository.GetAllAsync();
            var selected = all.Where(x => ids.Contains(x.IdInsumo)).ToList();
            return await UpdateBatchAsync(selected);
        }

        public async Task<NormalizedSupply?> UpdateSingleAsync(int idInsumo)
        {
            var item = await _repository.GetByIdInsumoAsync(idInsumo);
            if (item == null) return null;

            var updated = await UpdateBatchAsync(new List<NormalizedSupply> { item });
            return updated.FirstOrDefault();
        }

        private async Task<List<NormalizedSupply>> UpdateBatchAsync(List<NormalizedSupply> insumos)
        {
            var updatedList = new List<NormalizedSupply>();
            const int batchSize = 5;

            for (int i = 0; i < insumos.Count; i += batchSize)
            {
                var batch = insumos.Skip(i).Take(batchSize).ToList();

                var prompt = GeneratePrompt(batch);
                var resultJson = await _openAI.AnalyzeTextAsync(prompt, "gpt-4");

                var parsed = ParseOpenAIResponse(resultJson);
                foreach (var item in batch)
                {
                    var match = parsed.FirstOrDefault(x =>
                        x.nombre.Trim().ToLower() == item.Nombre.Trim().ToLower());

                    if (!string.IsNullOrWhiteSpace(match.nombre))
                    {
                        item.PrecioEstimado = match.precio;
                        item.VidaUtilMeses = match.vidaUtil;
                        item.PrecioGeneradoPorIA = true;
                        item.VidaUtilGeneradaPorIA = true;

                        await _repository.UpsertAsync(item);
                        updatedList.Add(item);
                    }
                }

            }

            return updatedList;
        }

        private string GeneratePrompt(List<NormalizedSupply> items)
        {
            var listado = string.Join(",\n", items.Select(x =>
                $"  {{ \"nombre\": \"{x.Nombre}\", \"descripcion\": \"{x.Descripcion}\" }}"));

            return $@"
Dame una lista de precios estimados en bolivianos y vida útil en meses para estos insumos de laboratorio.
Devuelve solo un JSON con un array llamado ""resultados"", sin ningún texto adicional.

Entrada:
[
{listado}
]

Formato de respuesta:
{{
  ""resultados"": [
    {{ ""nombre"": ""Breadboard"", ""precio"": 12.5, ""vidaUtil"": 18 }},
    {{ ""nombre"": ""KIT RASPBERRY"", ""precio"": 350, ""vidaUtil"": 24 }}
  ]
}}
";
        }

        


        private List<(string nombre, decimal precio, int vidaUtil)> ParseOpenAIResponse(string json)
        {
            var result = new List<(string, decimal, int)>();

            try
            {
                var root = JsonDocument.Parse(json).RootElement;
                var array = root.GetProperty("resultados").EnumerateArray();

                foreach (var item in array)
                {
                    var nombre = item.GetProperty("nombre").GetString() ?? "";
                    var precio = item.GetProperty("precio").GetDecimal();
                    var vidaUtil = item.GetProperty("vidaUtil").GetInt32();

                    result.Add((nombre, precio, vidaUtil));
                }
            }
            catch
            {
                // Si falla, retornar vacío
            }

            return result;
        }
    }
}
