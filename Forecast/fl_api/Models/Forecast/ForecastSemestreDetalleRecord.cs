namespace fl_api.Models.Forecast
{
    public class ForecastSemestreDetalleRecord
    {
        public string InsumoNombre { get; set; } = null!;
        public List<PronosticoMensualItem> PronosticoMensual { get; set; } = new();
        public DateTime FechaRegistro { get; set; }
    }
}
