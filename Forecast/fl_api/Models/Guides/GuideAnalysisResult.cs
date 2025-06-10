using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;
using System.Text.Json.Serialization;

namespace fl_api.Models.Guides
{
    public class GuideAnalysisResult
    {
        public ObjectId Id { get; set; }
        public ObjectId GuideFileId { get; set; }

        public string RegistryCode { get; set; } = null!;
        public string Version { get; set; } = null!;
        public string Subject { get; set; } = null!;

        public string Type { get; set; } = null!;
        public string Faculty { get; set; } = null!;
        public string Career { get; set; } = null!;

        public List<PracticeItem> Practices { get; set; } = new();
        public string ModelUsed { get; set; } = null!;
        public DateTime CreatedAt { get; set; }

        public string RawText { get; set; } = null!;
        public BsonDocument RawJson { get; set; } = null!;
    }


}
