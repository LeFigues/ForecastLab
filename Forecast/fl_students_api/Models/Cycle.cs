using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;

namespace fl_students_api.Models
{
    public class Cycle
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public ObjectId Id { get; set; }

        public int Year { get; set; }

        public int Semester { get; set; } // 1 o 2
    }
}
