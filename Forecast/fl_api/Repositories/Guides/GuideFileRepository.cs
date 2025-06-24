using fl_api.Interfaces.Guides;
using fl_api.Models.Guides;
using MongoDB.Driver;

namespace fl_api.Repositories.Guides
{
    public class GuideFileRepository : IGuideFileRepository
    {
        private readonly IMongoCollection<GuideFileRecord> _collection;

        public GuideFileRepository(IConfiguration config)
        {
            var client = new MongoClient(config["MongoDbSettings:ConnectionString"]);
            var db = client.GetDatabase(config["MongoDbSettings:DatabaseName"]);
            _collection = db.GetCollection<GuideFileRecord>("GuideFiles");
        }

        public async Task SaveAsync(GuideFileRecord file)
        {
            await _collection.InsertOneAsync(file);
        }

        public async Task<GuideFileRecord?> GetByIdAsync(string id)
        {
            var objectId = MongoDB.Bson.ObjectId.Parse(id);
            return await _collection.Find(f => f.Id == objectId).FirstOrDefaultAsync();
        }

        public async Task<List<GuideFileRecord>> GetAllAsync()
        {
            return await _collection.Find(_ => true).ToListAsync();
        }
        public async Task<List<GuideFileRecord>> GetByCycleAndFacultyAsync(string ciclo, string faculty)
        {
            var filter = Builders<GuideFileRecord>.Filter.Eq(x => x.Cycle, ciclo) &
                         Builders<GuideFileRecord>.Filter.Eq(x => x.Faculty, faculty);
            return await _collection.Find(filter).ToListAsync();
        }

    }
}
