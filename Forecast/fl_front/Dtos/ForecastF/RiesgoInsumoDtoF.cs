namespace fl_front.Dtos.ForecastF
{
    public class RiesgoInsumoDtoF
    {
        public string InsumoNombre { get; set; } = string.Empty;
        public int StockActual { get; set; }
        public double UsoMensualPromedio { get; set; }
        public double MesesSobrantes { get; set; }
        public string Riesgo { get; set; } = string.Empty;
    }
}
