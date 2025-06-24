using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;

namespace fl_students_api.Models
{
    public class Faculty
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public ObjectId Id { get; set; }

        public string Name { get; set; } = null!;
    }
}
