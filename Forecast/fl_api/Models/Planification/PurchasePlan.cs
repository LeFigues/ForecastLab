using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;

namespace fl_api.Models.Planification
{
    public class PurchasePlan
    {
        [BsonId]
        public ObjectId Id { get; set; }

        public string Insumo { get; set; } = null!;
        public int UnidadesAComprar { get; set; }
        public DateTime FechaSugeridaCompra { get; set; }
        public string? ProveedorSugerido { get; set; }
        public string Estado { get; set; } = "Pendiente";
    }
}
