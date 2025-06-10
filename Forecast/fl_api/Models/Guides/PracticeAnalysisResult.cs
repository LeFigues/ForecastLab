using MongoDB.Bson;

namespace fl_api.Models.Guides
{
    public class PracticeAnalysisResult
    {
        public ObjectId Id { get; set; }
        public ObjectId GuideFileId { get; set; }

        public string RegistryCode { get; set; } = null!;
        public string Version { get; set; } = null!;
        public string Subject { get; set; } = null!;
        public int PracticeNumber { get; set; }
        public string PracticeTitle { get; set; } = null!;

        public List<MaterialItem> Equipment { get; set; } = new();
        public List<MaterialItem> Supplies { get; set; } = new();
        public List<MaterialItem> Reagents { get; set; } = new();

        public int StudentsPerGroup { get; set; }
        public int GroupCount { get; set; }

        public string ModelUsed { get; set; } = null!;
        public DateTime CreatedAt { get; set; }

        public string RawText { get; set; } = null!;
        public BsonDocument RawJson { get; set; } = null!;
    }


}
