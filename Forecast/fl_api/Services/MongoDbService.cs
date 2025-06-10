using fl_api.Configurations;
using fl_api.Interfaces;
using Microsoft.Extensions.Options;
using MongoDB.Driver;

namespace fl_api.Services
{
    public class MongoDbService : IMongoDbService
    {
        private readonly IMongoClient _client;
        private readonly IMongoDatabase _database;

        public MongoDbService(IOptions<MongoDbSettings> options)
        {
            var settings = options.Value;
            _client = new MongoClient(settings.ConnectionString);
            _database = _client.GetDatabase(settings.DatabaseName);
        }

        public IMongoCollection<T> GetCollection<T>(string name)
            => _database.GetCollection<T>(name);

        public async Task<bool> CanConnectAsync()
        {
            try
            {
                await _client.ListDatabaseNamesAsync();
                return true;
            }
            catch
            {
                return false;
            }
        }
    }
}
