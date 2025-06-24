using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;

namespace fl_api.Models.Planification
{
    public class SupplyPurchasePlan
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; } = string.Empty;

        public string StudentDemandReportId { get; set; } = string.Empty;
        public string Cycle { get; set; } = string.Empty;
        public string Faculty { get; set; } = string.Empty;
        public DateTime GeneratedAt { get; set; } = DateTime.UtcNow;

        public List<SupplyPurchaseItem> Items { get; set; } = new();
    }
}
