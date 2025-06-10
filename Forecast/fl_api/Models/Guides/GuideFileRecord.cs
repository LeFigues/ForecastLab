using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;

namespace fl_api.Models.Guides
{
    public class GuideFileRecord
    {
        [BsonId]
        public ObjectId Id { get; set; }

        public string FileName { get; set; } = null!;
        public string FilePath { get; set; } = null!;
        public string Tipo { get; set; } = null!; // "guia" o "practica"
        public DateTime UploadedAt { get; set; }

        public string? Faculty { get; set; }
        public string? Career { get; set; }
        public string? Subject { get; set; }
    }
}
