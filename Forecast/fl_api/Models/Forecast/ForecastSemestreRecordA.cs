using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;

namespace fl_api.Models.Forecast
{
    public class ForecastSemestreRecordA
    {
        [BsonId]
        public ObjectId Id { get; set; }

        public string InsumoNombre { get; set; } = null!;
        public int StockActual { get; set; }
        public int TotalPronosticadoSemestre { get; set; }
        public int UnidadesAComprar { get; set; }
        public decimal CostoEstimado { get; set; }

        // Lista de pronósticos mensuales serializada en JSON o como BsonArray
        public List<PronosticoMensualItem> PronosticoMensual { get; set; } = new();

        public DateTime FechaRegistro { get; set; } = DateTime.UtcNow;
    }
}
