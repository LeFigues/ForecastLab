using fl_api.Interfaces.Validation;
using fl_api.Models.Validation;
using MongoDB.Bson;
using MongoDB.Driver;

namespace fl_api.Repositories.Validation
{
    public class ForecastValidationRepository : IForecastValidationRepository
    {
        private readonly IMongoCollection<ForecastValidation> _collection;

        public ForecastValidationRepository(IConfiguration config)
        {
            var client = new MongoClient(config.GetConnectionString("MongoDb"));
            var db = client.GetDatabase(config["MongoDbSettings:DatabaseName"]);
            _collection = db.GetCollection<ForecastValidation>("ForecastValidations");
        }

        public async Task SaveAsync(ForecastValidation validation)
            => await _collection.InsertOneAsync(validation);

        public async Task<List<ForecastValidation>> GetAllAsync()
            => await _collection.Find(_ => true).SortByDescending(v => v.CreatedAt).ToListAsync();

        public async Task<ForecastValidation?> GetByIdAsync(string id)
        {
            var objectId = ObjectId.Parse(id);
            return await _collection.Find(x => x.Id == objectId).FirstOrDefaultAsync();
        }

        public async Task AddCommentAsync(string id, string comment)
        {
            var objectId = ObjectId.Parse(id);
            var update = Builders<ForecastValidation>.Update.Set(x => x.UserComment, comment);
            await _collection.UpdateOneAsync(x => x.Id == objectId, update);
        }
    }
}
