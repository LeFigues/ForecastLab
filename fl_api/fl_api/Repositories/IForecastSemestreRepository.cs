using fl_api.Models;

namespace fl_api.Repositories
{
    public interface IForecastSemestreRepository
    {
        Task SaveManyAsync(IEnumerable<ForecastSemestreRecord> records);

        // Nuevo método para leer todos los registros guardados
        Task<List<ForecastSemestreRecord>> GetAllAsync();
    }
}
