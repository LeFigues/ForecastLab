namespace fl_front.Models
{
    public class ForecastInsumoSemestreDto
    {
        public string InsumoNombre { get; set; } = string.Empty;
        public int StockActual { get; set; }
        public int TotalPronosticadoSemestre { get; set; }
        public int UnidadesAComprar { get; set; }
        public decimal CostoEstimado { get; set; }
        public List<PronosticoMensualDto> PronosticoMensual { get; set; } = new();
    }

    public class PronosticoMensualDto
    {
        public DateTime PeriodStart { get; set; }
        public int ForecastedQuantity { get; set; }
    }
}
