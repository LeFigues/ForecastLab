using fl_api.Interfaces.Planification;
using fl_api.Models.Planification;
using MongoDB.Bson;
using MongoDB.Driver;

namespace fl_api.Repositories.Planification
{
    public class PurchaseOrderRepository : IPurchaseOrderRepository
    {
        private readonly IMongoCollection<PurchaseOrder> _collection;

        public PurchaseOrderRepository(IConfiguration config)
        {
            var client = new MongoClient(config["MongoDbSettings:ConnectionString"]);
            var db = client.GetDatabase(config["MongoDbSettings:DatabaseName"]);
            _collection = db.GetCollection<PurchaseOrder>("PurchaseOrders");
        }

        public async Task SaveAsync(PurchaseOrder order)
        {
            await _collection.InsertOneAsync(order);
        }

        public async Task<List<PurchaseOrder>> GetAllAsync()
        {
            return await _collection.Find(_ => true).ToListAsync();
        }
        public async Task UpdateEstadoAsync(ObjectId id, string nuevoEstado)
        {
            var update = Builders<PurchaseOrder>.Update.Set(p => p.Estado, nuevoEstado);
            await _collection.UpdateOneAsync(p => p.Id == id, update);
        }
        public async Task<PurchaseOrder?> GetByIdAsync(ObjectId id)
        {
            return await _collection.Find(p => p.Id == id).FirstOrDefaultAsync();
        }

    }

}
