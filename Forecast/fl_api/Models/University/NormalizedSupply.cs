using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;

namespace fl_api.Models.University
{
    public class NormalizedSupply
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        [BsonIgnoreIfDefault]
        public string? Id { get; set; }

        [BsonElement("id_insumo")]
        public int IdInsumo { get; set; }

        [BsonElement("nombre")]
        public string Nombre { get; set; } = string.Empty;

        [BsonElement("nombre_normalizado")]
        public string NombreNormalizado { get; set; } = string.Empty;

        [BsonElement("descripcion")]
        public string Descripcion { get; set; } = string.Empty;

        [BsonElement("tipo")]
        public string Tipo { get; set; } = string.Empty;

        [BsonElement("unidad_medida")]
        public string UnidadMedida { get; set; } = string.Empty;

        [BsonElement("stock_total")]
        public int StockTotal { get; set; }

        [BsonElement("stock_minimo")]
        public int StockMinimo { get; set; }

        [BsonElement("stock_maximo")]
        public int StockMaximo { get; set; }

        [BsonElement("precio_estimado")]
        public decimal PrecioEstimado { get; set; }

        [BsonElement("vida_util_meses")]
        public int VidaUtilMeses { get; set; }

        [BsonElement("fecha_compra")]
        public int AñoCompra { get; set; } = 2023;

        [BsonElement("categoria")]
        public string? Categoria { get; set; }

        [BsonElement("frecuencia_uso")]
        public int? FrecuenciaUso { get; set; }

        [BsonElement("cantidad_usada_promedio")]
        public decimal? CantidadUsadaPromedio { get; set; }

        [BsonElement("precio_generado_por_ia")]
        public bool PrecioGeneradoPorIA { get; set; }

        [BsonElement("vida_util_generada_por_ia")]
        public bool VidaUtilGeneradaPorIA { get; set; }
    }
}
