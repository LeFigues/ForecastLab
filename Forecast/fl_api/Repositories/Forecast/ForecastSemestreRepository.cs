using fl_api.Interfaces.University;
using fl_api.Models.Forecast;
using MongoDB.Driver;

namespace fl_api.Repositories.Forecast
{
    public class ForecastSemestreRepository : IForecastSemestreRepository
    {
        private readonly IMongoCollection<ForecastSemestreRecord> _collection;
        private readonly IMongoDatabase _database;

        public ForecastSemestreRepository(IConfiguration config)
        {
            var client = new MongoClient(config["MongoDbSettings:ConnectionString"]);
            _database = client.GetDatabase(config["MongoDbSettings:DatabaseName"]);
            _collection = _database.GetCollection<ForecastSemestreRecord>("ForecastSemestre");
        }

        public async Task SaveManyAsync(IEnumerable<ForecastSemestreRecord> records)
        {
            var list = records.ToList();
            if (list.Any())
            {
                await _collection.InsertManyAsync(list);
            }
        }

        public async Task<List<ForecastSemestreRecord>> GetAllAsync()
        {
            return await _collection.Find(_ => true).ToListAsync();
        }

        public async Task<List<ForecastSemestreRecord>> GetForecastPendienteAsync()
        {
            return await _collection
                .Find(f => f.UnidadesAComprar > 0)
                .ToListAsync();
        }

        public Task<List<ForecastSemestreRecord>> GetForecastsInRangeAsync(DateTime from, DateTime to, string? labId)
        {
            throw new NotImplementedException();
        }

        public IMongoCollection<T> GetRawCollection<T>(string name)
            => _database.GetCollection<T>(name);
    }
}
