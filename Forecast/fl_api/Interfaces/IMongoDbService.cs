using MongoDB.Driver;

namespace fl_api.Interfaces
{
    public interface IMongoDbService
    {
        Task<bool> CanConnectAsync();
        IMongoCollection<T> GetCollection<T>(string name);
    }
}
