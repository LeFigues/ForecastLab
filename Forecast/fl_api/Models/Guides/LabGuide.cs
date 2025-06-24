using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;
using Newtonsoft.Json;

namespace fl_api.Models.Guides
{
    public class LabGuide
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string? Id { get; set; }

        [BsonElement("laboratorio")]
        [JsonProperty("laboratorio")]
        public string LabName { get; set; } = null!;

        [BsonElement("codigo")]
        [JsonProperty("codigo")]
        public string Code { get; set; } = null!;

        [BsonElement("guideFileId")]
        public string GuideFileId { get; set; } = null!;

        [BsonElement("version")]
        [JsonProperty("version")]
        public string Version { get; set; } = null!;

        [BsonElement("practicas")]
        [JsonProperty("practicas")]
        public List<LabPractice> Practices { get; set; } = new();

        [BsonElement("faculty")]
        public string? Faculty { get; set; }

        [BsonElement("career")]
        public string? Career { get; set; }

        [BsonElement("subject")]
        public string? Subject { get; set; }

        [BsonElement("cycle")]
        public string? Cycle { get; set; }
    }
}
