using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;

namespace fl_api.Configurations
{
    public class UniversityApiConfig
    {
        [BsonId]
        public ObjectId Id { get; set; }

        public string BaseUrl { get; set; } = null!;
        public string? ApiKey { get; set; }
    }
}
