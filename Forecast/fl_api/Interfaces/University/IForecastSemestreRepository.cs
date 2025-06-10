using fl_api.Models.Forecast;
using MongoDB.Driver;

namespace fl_api.Interfaces.University
{
    public interface IForecastSemestreRepository
    {
        Task SaveManyAsync(IEnumerable<ForecastSemestreRecord> records);
        Task<List<ForecastSemestreRecord>> GetAllAsync();
        Task<List<ForecastSemestreRecord>> GetForecastPendienteAsync();
        Task<List<ForecastSemestreRecord>> GetForecastsInRangeAsync(DateTime from, DateTime to, string? labId);
        IMongoCollection<T> GetRawCollection<T>(string name);

    }

}
