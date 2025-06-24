using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;

namespace fl_students_api.Models
{
    public class Teacher
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public ObjectId Id { get; set; }

        public string FullName { get; set; } = null!;
    }
}
