using fl_api.Interfaces.Purchases;
using fl_api.Models.Planification;
using MongoDB.Bson;
using MongoDB.Driver;

namespace fl_api.Repositories.Purchases
{
    public class SupplyPurchasePlanRepository : ISupplyPurchasePlanRepository
    {
        private readonly IMongoCollection<SupplyPurchasePlan> _collection;

        public SupplyPurchasePlanRepository(IConfiguration config)
        {
            var client = new MongoClient(config["MongoDbSettings:ConnectionString"]);
            var db = client.GetDatabase(config["MongoDbSettings:DatabaseName"]);
            _collection = db.GetCollection<SupplyPurchasePlan>("PurchaseOrders");
        }

        public async Task SaveAsync(SupplyPurchasePlan plan) =>
            await _collection.InsertOneAsync(plan);

        public async Task<List<SupplyPurchasePlan>> GetAllAsync() =>
            await _collection.Find(_ => true).ToListAsync();

        public async Task<SupplyPurchasePlan?> GetByIdAsync(string id)
        {
            if (!ObjectId.TryParse(id, out var objectId))
                return null;

            return await _collection.Find(x => x.Id == objectId.ToString()).FirstOrDefaultAsync();
        }
    }
}
