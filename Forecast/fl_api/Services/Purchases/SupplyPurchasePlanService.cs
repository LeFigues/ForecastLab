using fl_api.Interfaces;
using fl_api.Interfaces.Purchases;
using fl_api.Interfaces.Students;
using fl_api.Models.Planification;
using System.Text.Json;

namespace fl_api.Services.Purchases
{
    public class SupplyPurchasePlanService : ISupplyPurchasePlanService
    {
        private readonly IStudentDemandForecastService _forecastService;
        private readonly IOpenAIService _openAIService;
        private readonly ISupplyPurchasePlanRepository _repo;

        public SupplyPurchasePlanService(
            IStudentDemandForecastService forecastService,
            IOpenAIService openAIService,
            ISupplyPurchasePlanRepository repo)
        {
            _forecastService = forecastService;
            _openAIService = openAIService;
            _repo = repo;
        }

        public async Task<SupplyPurchasePlan> GeneratePlanAsync(string reportId)
        {
            var report = await _forecastService.GetByIdAsync(reportId)
                         ?? throw new Exception("Reporte no encontrado");

            var itemsToEstimate = report.Items
                .Where(i => !i.ExistsInSystem)
                .Select(i => (i.Description, i.Unit))
                .ToList();

            var prompt = GeneratePrompt(itemsToEstimate);
            var gptResponse = await _openAIService.AnalyzeTextAsync(prompt, "gpt-4");

            var parsed = ParseOpenAIResponse(gptResponse);

            var plan = new SupplyPurchasePlan
            {
                StudentDemandReportId = report.Id!,
                Cycle = report.Cycle,
                Faculty = report.Faculty,
                GeneratedAt = DateTime.UtcNow,
                Items = report.Items.Select(item =>
                {
                    var extra = (int)Math.Ceiling(item.RequiredQuantity * 0.1);
                    var recommended = item.RequiredQuantity + extra;

                    var priceMatch = parsed.FirstOrDefault(p =>
                        p.nombre.Trim().ToLower() == item.Description.Trim().ToLower());

                    return new SupplyPurchaseItem
                    {
                        Description = item.Description,
                        Unit = item.Unit,
                        RequiredQuantity = item.RequiredQuantity,
                        RecommendedQuantity = recommended,
                        EstimatedPrice = item.ExistsInSystem ? 0.0m : priceMatch.precio,
                        ExistsInSystem = item.ExistsInSystem,
                        IdInsumo = item.IdInsumo
                    };
                }).ToList()
            };

            await _repo.SaveAsync(plan);
            return plan;
        }

        private string GeneratePrompt(List<(string descripcion, string unidad)> items)
        {
            var listado = string.Join(",\n", items.Select(x =>
                $"  {{ \"nombre\": \"{x.descripcion}\", \"unidad\": \"{x.unidad}\" }}"));

            return $@"
Eres un experto en compras universitarias. Estima el precio promedio en bolivianos por unidad para los siguientes insumos de laboratorio.
Devuelve solo un JSON con un array llamado ""resultados"", sin ningún texto adicional.

Entrada:
[
{listado}
]

Formato de respuesta:
{{
  ""resultados"": [
    {{ ""nombre"": ""Cable UTP Cat 6"", ""precio"": 3.5 }},
    {{ ""nombre"": ""Crimpadora RJ45"", ""precio"": 45.0 }}
  ]
}}";
        }

        private List<(string nombre, decimal precio)> ParseOpenAIResponse(string json)
        {
            var result = new List<(string, decimal)>();

            try
            {
                var root = JsonDocument.Parse(json).RootElement;
                var array = root.GetProperty("resultados").EnumerateArray();

                foreach (var item in array)
                {
                    var nombre = item.GetProperty("nombre").GetString() ?? "";
                    var precio = item.GetProperty("precio").GetDecimal();
                    result.Add((nombre, precio));
                }
            }
            catch
            {
                // Ignorar errores de deserialización
            }

            return result;
        }
    }
}
