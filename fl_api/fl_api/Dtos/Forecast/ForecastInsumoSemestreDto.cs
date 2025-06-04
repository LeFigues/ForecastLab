namespace fl_api.Dtos.Forecast
{
    public class ForecastInsumoSemestreDto
    {
        public string InsumoNombre { get; set; } = null!;
        public int StockActual { get; set; }
        public int TotalPronosticadoSemestre { get; set; }
        public int UnidadesAComprar { get; set; }
        public decimal CostoEstimado { get; set; }
        public List<ForecastPointDto> PronosticoMensual { get; set; } = new();
    }
}
