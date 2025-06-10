using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;

namespace fl_api.Models.Planification
{
    public class PurchaseOrder
    {
        [BsonId]
        public ObjectId Id { get; set; }

        public string NumeroOrden { get; set; } = null!;
        public string Proveedor { get; set; } = null!;
        public DateTime FechaEmision { get; set; }
        public string Estado { get; set; } = "Pendiente"; // Pendiente, Enviado, Recibido
        public decimal TotalMonto { get; set; }
        public List<PurchaseOrderItem> Items { get; set; } = new();
        public string? Notas { get; set; }
    }

}
