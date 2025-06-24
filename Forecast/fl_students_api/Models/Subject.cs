using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;

namespace fl_students_api.Models
{
    public class Subject
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public ObjectId Id { get; set; }

        public string Name { get; set; } = null!;

        public ObjectId CareerId { get; set; }

        public int Semester { get; set; } // De qué semestre de la carrera es esta materia
    }
}
