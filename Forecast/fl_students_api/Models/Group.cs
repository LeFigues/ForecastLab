using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;

namespace fl_students_api.Models
{
    public class Group
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public ObjectId Id { get; set; }

        public ObjectId SubjectId { get; set; }

        public ObjectId CycleId { get; set; }

        public ObjectId TeacherId { get; set; }

        public int StudentCount { get; set; }
    }
}
