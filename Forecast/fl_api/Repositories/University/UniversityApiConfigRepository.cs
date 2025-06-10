using fl_api.Configurations;
using fl_api.Interfaces.University;
using MongoDB.Driver;

namespace fl_api.Repositories.University
{
    public class UniversityApiConfigRepository : IUniversityApiConfigRepository
    {
        private readonly IMongoCollection<UniversityApiConfig> _collection;

        public UniversityApiConfigRepository(IMongoClient client, IConfiguration config)
        {
            var db = client.GetDatabase(config["MongoDbSettings:DatabaseName"]);
            _collection = db.GetCollection<UniversityApiConfig>("UniversityApiConfig");
        }

        public async Task<UniversityApiConfig?> GetAsync()
            => await _collection.Find(_ => true).FirstOrDefaultAsync();

        public async Task SaveAsync(UniversityApiConfig config)
        {
            var existing = await GetAsync();
            if (existing is null)
                await _collection.InsertOneAsync(config);
            else
                await _collection.ReplaceOneAsync(x => x.Id == existing.Id, config);
        }
    }

}
