using fl_api.Interfaces.Planification;
using fl_api.Models.Planification;
using MongoDB.Driver;

namespace fl_api.Repositories.Planification
{
    public class PurchasePlanRepository : IPurchasePlanRepository
    {
        private readonly IMongoCollection<PurchasePlan> _collection;

        public PurchasePlanRepository(IConfiguration config)
        {
            var client = new MongoClient(config["MongoDbSettings:ConnectionString"]);
            var db = client.GetDatabase(config["MongoDbSettings:DatabaseName"]);
            _collection = db.GetCollection<PurchasePlan>("PurchasePlans");
        }

        public async Task SaveManyAsync(IEnumerable<PurchasePlan> planes)
        {
            var lista = planes.ToList();
            if (lista.Any())
            {
                await _collection.InsertManyAsync(lista);
            }
        }

        public async Task<List<PurchasePlan>> GetAllAsync() => await _collection.Find(_ => true).ToListAsync();

        public async Task<List<string>> GetInsumosPlanificadosAsync()
        {
            var insumos = await _collection
                .Find(p => p.Estado == "Pendiente")
                .Project(p => p.Insumo)
                .ToListAsync();

            return insumos.Distinct().ToList(); // por si hay duplicados anteriores
        }

    }

}
