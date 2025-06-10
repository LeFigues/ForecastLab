using fl_api.Interfaces.Reports;
using fl_api.Models.Reports;
using MongoDB.Driver;

namespace fl_api.Repositories.Reports
{
    public class PrecioEstimadoRepository : IPrecioEstimadoRepository
    {
        private readonly IMongoCollection<PrecioEstimadoRecord> _collection;

        public PrecioEstimadoRepository(IConfiguration config)
        {
            var client = new MongoClient(config["MongoDbSettings:ConnectionString"]);
            var db = client.GetDatabase(config["MongoDbSettings:DatabaseName"]);
            _collection = db.GetCollection<PrecioEstimadoRecord>("PreciosEstimados");
        }

        public async Task<List<PrecioEstimadoRecord>> GetAllAsync() =>
            await _collection.Find(_ => true).ToListAsync();

        public async Task<PrecioEstimadoRecord?> GetByNombreAsync(string nombre) =>
            await _collection.Find(p => p.Nombre == nombre).FirstOrDefaultAsync();

        public async Task SaveManyAsync(IEnumerable<PrecioEstimadoRecord> precios)
        {
            if (precios.Any())
                await _collection.InsertManyAsync(precios);
        }
    }
}
