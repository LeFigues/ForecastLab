using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;

namespace fl_api.Models.Students
{
    public class StudentBasedDemandReport
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string? Id { get; set; }

        public string Cycle { get; set; } = null!;
        public string Faculty { get; set; } = null!;
        public DateTime GeneratedAt { get; set; }
        public List<StudentDemandItem> Items { get; set; } = new();
    }
}
