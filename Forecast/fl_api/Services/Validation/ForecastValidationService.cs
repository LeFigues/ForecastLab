using fl_api.Dtos.Validation;
using fl_api.Interfaces.University;
using fl_api.Interfaces.Validation;
using fl_api.Interfaces;
using fl_api.Models.Validation;
using MongoDB.Bson;
using University.Interfaces;

namespace fl_api.Services.Validation
{
    public class ForecastValidationService : IForecastValidationService
    {
        private readonly IForecastValidationRepository _repo;
        private readonly IUniversityApiClient _university;
        private readonly IForecastSemestreRepository _forecastRepo;
        private readonly IOpenAIService _openai;

        public ForecastValidationService(
            IForecastValidationRepository repo,
            IUniversityApiClient university,
            IForecastSemestreRepository forecastRepo,
            IOpenAIService openai)
        {
            _repo = repo;
            _university = university;
            _forecastRepo = forecastRepo;
            _openai = openai;
        }

        //public async Task<ForecastValidation> ValidateForecastAsync(ForecastValidationRequestDto request)
        //{
        //    var forecast = await _forecastRepo.GetForecastsInRangeAsync(request.From, request.To, request.LabId);
        //    var movimientos = await _university.GetMovimientosInventarioAsync();

        //    var reales = movimientos
        //        .Where(x =>
        //            x.TipoMovimiento == "PRESTAMO" &&
        //            x.FechaEntregado >= request.From &&
        //            x.FechaEntregado <= request.To &&
        //            (request.LabId == null || x.LaboratorioId == request.LabId))
        //        .GroupBy(x => new { x.InsumoNombre, Mes = x.FechaEntregado.ToString("yyyy-MM") })
        //        .ToDictionary(g => g.Key, g => g.Sum(x => x.Cantidad));

        //    var items = new List<ForecastValidationItem>();

        //    foreach (var f in forecast)
        //    {
        //        foreach (var mensual in f.PronosticoMensual)
        //        {
        //            var mes = mensual.PeriodStart.ToString("yyyy-MM");
        //            var key = new { InsumoNombre = f.InsumoNombre, Mes = mes };

        //            reales.TryGetValue(key, out int real);
        //            var item = new ForecastValidationItem
        //            {
        //                Insumo = f.InsumoNombre,
        //                Mes = mes,
        //                CantidadPronosticada = mensual.ForecastedQuantity,
        //                CantidadConsumida = real
        //            };

        //            items.Add(item);
        //        }
        //    }

        //    double mape = items
        //        .Where(i => i.CantidadConsumida > 0)
        //        .Select(i => i.ErrorPorcentual)
        //        .DefaultIfEmpty(0)
        //        .Average();

        //    double rmse = Math.Sqrt(items
        //        .Select(i => Math.Pow(i.CantidadPronosticada - i.CantidadConsumida, 2))
        //        .DefaultIfEmpty(0)
        //        .Average());

        //    var resumen = new
        //    {
        //        mape = Math.Round(mape, 2),
        //        rmse = Math.Round(rmse, 2)
        //    };

        //    var prompt = $"Dado un MAPE de {resumen.mape}% y un RMSE de {resumen.rmse}, responde si el nivel de error es aceptable para una predicción de consumo de insumos de laboratorio. Responde solo con 'Sí' o 'No'.";
        //    var aiResult = await _openai.GetCompletionAsync(prompt);

        //    var validation = new ForecastValidation
        //    {
        //        Id = ObjectId.GenerateNewId(),
        //        From = request.From,
        //        To = request.To,
        //        LabId = request.LabId,
        //        Items = items,
        //        MAPE = resumen.mape,
        //        RMSE = resumen.rmse,
        //        AiSuggestion = aiResult?.Trim()
        //    };

        //    await _repo.SaveAsync(validation);
        //    return validation;
        //}

        public async Task<List<ForecastValidation>> GetAllAsync()
            => await _repo.GetAllAsync();

        public async Task AddCommentAsync(string validationId, string comment)
            => await _repo.AddCommentAsync(validationId, comment);
    }
}
