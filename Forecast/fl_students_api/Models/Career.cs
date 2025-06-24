using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;

namespace fl_students_api.Models
{
    public class Career
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public ObjectId Id { get; set; }

        public string Name { get; set; } = null!;

        public ObjectId FacultyId { get; set; }

        public int TotalSemesters { get; set; }
    }
}
