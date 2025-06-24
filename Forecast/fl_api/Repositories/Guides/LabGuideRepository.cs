using fl_api.Interfaces.Guides;
using fl_api.Models.Guides;
using MongoDB.Driver;

namespace fl_api.Repositories.Guides
{
    public class LabGuideRepository : ILabGuideRepository
    {
        private readonly IMongoCollection<LabGuide> _collection;

        public LabGuideRepository(IConfiguration config)
        {
            var client = new MongoClient(config["MongoDbSettings:ConnectionString"]);
            var db = client.GetDatabase(config["MongoDbSettings:DatabaseName"]);
            _collection = db.GetCollection<LabGuide>("LabGuides");
        }

        public async Task SaveAsync(LabGuide guide)
        {
            await _collection.InsertOneAsync(guide);
        }

        public async Task<List<LabGuide>> GetAllAsync()
        {
            return await _collection.Find(_ => true).ToListAsync();
        }

        public async Task<LabGuide?> GetByIdAsync(string id)
        {
            var objectId = MongoDB.Bson.ObjectId.Parse(id);
            return await _collection.Find(g => g.Id == id).FirstOrDefaultAsync();
        }
        public async Task<LabGuide?> GetByGuideFileIdAsync(string guideFileId)
        {
            var filter = Builders<LabGuide>.Filter.Eq("GuideFileId", guideFileId);
            return await _collection.Find(filter).FirstOrDefaultAsync();
        }
        public async Task<List<LabGuide>> GetByCycleAndFacultyAsync(string ciclo, string facultad)
        {
            var filter = Builders<LabGuide>.Filter.Eq("Cycle", ciclo) &
                         Builders<LabGuide>.Filter.Eq("Faculty", facultad);
            return await _collection.Find(filter).ToListAsync();
        }

    }
}
