using fl_api.Interfaces.Planification;
using fl_api.Models.Planification;
using MongoDB.Driver;

namespace fl_api.Repositories.Planification
{
    public class SemesterPurchaseRepository : ISemesterPurchaseRepository
    {
        private readonly IMongoCollection<SemesterPurchasePlan> _collection;

        public SemesterPurchaseRepository(IConfiguration config)
        {
            var client = new MongoClient(config["MongoDbSettings:ConnectionString"]);
            var database = client.GetDatabase(config["MongoDbSettings:DatabaseName"]);
            _collection = database.GetCollection<SemesterPurchasePlan>("SemesterPurchasePlans");
        }

        public async Task SaveAsync(SemesterPurchasePlan plan)
        {
            await _collection.InsertOneAsync(plan);
        }

        public async Task<List<SemesterPurchasePlan>> GetAllAsync()
        {
            return await _collection.Find(_ => true).ToListAsync();
        }

        public async Task<SemesterPurchasePlan?> GetByIdAsync(string id)
        {
            return await _collection.Find(p => p.Id == id).FirstOrDefaultAsync();
        }
    }
}
