using fl_api.Interfaces.Guides;
using fl_api.Models.Guides;
using MongoDB.Driver;

namespace fl_api.Repositories.Guides
{
    public class GuideAnalysisRepository : IGuideAnalysisRepository
    {
        private readonly IMongoCollection<GuideAnalysisResult> _collection;

        public GuideAnalysisRepository(IConfiguration config)
        {
            var client = new MongoClient(config["MongoDbSettings:ConnectionString"]);
            var db = client.GetDatabase(config["MongoDbSettings:DatabaseName"]);
            _collection = db.GetCollection<GuideAnalysisResult>("GuideAnalyses");
        }

        public async Task SaveAsync(GuideAnalysisResult result)
        {
            await _collection.InsertOneAsync(result);
        }

        public async Task<GuideAnalysisResult?> GetByGuideFileIdAsync(string guideFileId)
        {
            var objectId = MongoDB.Bson.ObjectId.Parse(guideFileId);
            return await _collection.Find(x => x.GuideFileId == objectId).FirstOrDefaultAsync();
        }
    }
}
