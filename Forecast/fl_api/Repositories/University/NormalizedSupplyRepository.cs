using fl_api.Interfaces.University;
using fl_api.Models.Reports;
using fl_api.Models.University;
using MongoDB.Driver;

namespace fl_api.Repositories.University
{
    public class NormalizedSupplyRepository : INormalizedSupplyRepository
    {
        private readonly IMongoCollection<NormalizedSupply> _collection;

        public NormalizedSupplyRepository(IConfiguration config)
        {
            var client = new MongoClient(config["MongoDbSettings:ConnectionString"]);
            var db = client.GetDatabase(config["MongoDbSettings:DatabaseName"]);
            _collection = db.GetCollection<NormalizedSupply>("normalized_supplies");
        }

        public async Task<List<NormalizedSupply>> GetAllAsync()
        {
            return await _collection.Find(_ => true).ToListAsync();
        }

        public async Task<NormalizedSupply?> GetByIdInsumoAsync(int idInsumo)
        {
            return await _collection.Find(x => x.IdInsumo == idInsumo).FirstOrDefaultAsync();
        }

        public async Task InsertManyAsync(List<NormalizedSupply> supplies)
        {
            await _collection.InsertManyAsync(supplies);
        }

        public async Task UpsertAsync(NormalizedSupply supply)
        {
            var filter = Builders<NormalizedSupply>.Filter.Eq(x => x.IdInsumo, supply.IdInsumo);
            await _collection.ReplaceOneAsync(filter, supply, new ReplaceOptions { IsUpsert = true });
        }

        public async Task DeleteAllAsync()
        {
            await _collection.DeleteManyAsync(_ => true);
        }
    }
}
