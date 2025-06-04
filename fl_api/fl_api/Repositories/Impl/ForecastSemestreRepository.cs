using fl_api.Interfaces;
using fl_api.Models;
using MongoDB.Driver;

namespace fl_api.Repositories.Impl
{
    public class ForecastSemestreRepository : IForecastSemestreRepository
    {
        private readonly IMongoCollection<ForecastSemestreRecord> _collection;

        public ForecastSemestreRepository(IMongoDbService mongo)
        {
            // Usamos el mismo patrón que ForecastHistoricoRepository:
            _collection = mongo.GetCollection<ForecastSemestreRecord>("ForecastSemestre");
        }

        public async Task SaveManyAsync(IEnumerable<ForecastSemestreRecord> records)
        {
            if (records.Any())
            {
                await _collection.InsertManyAsync(records);
            }
        }

        public async Task<List<ForecastSemestreRecord>> GetAllAsync()
        {
            return await _collection
                .Find(_ => true)
                .ToListAsync();
        }
    }
}
