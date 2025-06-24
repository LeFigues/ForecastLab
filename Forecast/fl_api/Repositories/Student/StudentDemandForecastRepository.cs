using fl_api.Interfaces.Students;
using fl_api.Models.Students;
using MongoDB.Driver;

namespace fl_api.Repositories.Student
{
    public class StudentDemandForecastRepository : IStudentDemandForecastRepository
    {
        private readonly IMongoCollection<StudentDemandReport> _collection;

        public StudentDemandForecastRepository(IConfiguration config)
        {
            var client = new MongoClient(config["MongoDbSettings:ConnectionString"]);
            var db = client.GetDatabase(config["MongoDbSettings:DatabaseName"]);
            _collection = db.GetCollection<StudentDemandReport>("StudentDemandReports");
        }

        public async Task SaveAsync(StudentDemandReport report)
        {
            await _collection.InsertOneAsync(report);
        }

        public async Task<List<StudentDemandReport>> GetAllAsync()
        {
            return await _collection.Find(_ => true).ToListAsync();
        }

        public async Task<StudentDemandReport?> GetByIdAsync(string id)
        {
            return await _collection.Find(r => r.Id == id).FirstOrDefaultAsync();
        }
    }
}
