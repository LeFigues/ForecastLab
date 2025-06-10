using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;

namespace fl_api.Models.Guides
{
    public class GuideAnalysisRecord
    {
        [BsonId]
        public ObjectId Id { get; set; }

        public string GuideFileId { get; set; } = null!;
        public string RegistryCode { get; set; } = null!;
        public string Version { get; set; } = null!;
        public string Subject { get; set; } = null!;
        public string ModelUsed { get; set; } = null!;
        public string Type { get; set; } = null!;
        public DateTime CreatedAt { get; set; }
        public string RawText { get; set; } = null!;
        public List<GuideAnalysisValue> RawJson { get; set; } = new();
    }
}
