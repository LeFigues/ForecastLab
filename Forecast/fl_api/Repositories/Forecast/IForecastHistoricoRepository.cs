using fl_api.Models;

namespace fl_api.Repositories.Forecast
{
    public interface IForecastHistoricoRepository
    {
        Task SaveManyAsync(List<ForecastHistoricoRecord> records);
        Task<List<ForecastHistoricoRecord>> GetAllAsync();
    }
}
