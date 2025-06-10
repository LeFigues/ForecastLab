using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;

namespace fl_api.Models.Validation
{
    public class ForecastValidation
    {
        [BsonId]
        public ObjectId Id { get; set; }

        public DateTime From { get; set; }
        public DateTime To { get; set; }
        public string? LabId { get; set; }

        public List<ForecastValidationItem> Items { get; set; } = new();

        public double MAPE { get; set; }
        public double RMSE { get; set; }
        public string? AiSuggestion { get; set; }
        public string? UserComment { get; set; }

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    }
}
