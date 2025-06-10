using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;

namespace fl_api.Models.Reports
{
    public class PrecioEstimadoRecord
    {
        [BsonId]
        public ObjectId Id { get; set; }

        [BsonElement("nombre")]
        public string Nombre { get; set; } = null!;

        [BsonElement("precio")]
        public decimal Precio { get; set; }

        [BsonElement("fechaEstimacion")]
        public DateTime FechaEstimacion { get; set; } = DateTime.UtcNow;
    }
}
