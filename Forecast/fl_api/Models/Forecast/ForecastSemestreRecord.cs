using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;

namespace fl_api.Models.Forecast
{
    [BsonIgnoreExtraElements]
    public class ForecastSemestreRecord
    {
        [BsonId]
        public ObjectId Id { get; set; }

        public string InsumoNombre { get; set; } = null!;
        public int StockActual { get; set; }
        public int TotalPronosticadoSemestre { get; set; }
        public int UnidadesAComprar { get; set; }

        // NUEVO: agregar esto
        public decimal CostoEstimado { get; set; } = 0;
    }


}
