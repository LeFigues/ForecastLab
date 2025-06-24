using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;

namespace fl_api.Models.Planification
{
    public class SemesterPurchasePlan
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string? Id { get; set; }

        public string Cycle { get; set; } = null!;
        public string Faculty { get; set; } = null!;
        public DateTime GeneratedAt { get; set; }
        public List<PlannedItem> Items { get; set; } = new();
    }
}
