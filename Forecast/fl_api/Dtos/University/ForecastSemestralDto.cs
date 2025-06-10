namespace fl_api.Dtos.University
{
    public class ForecastSemestralDto
    {
        public string InsumoNombre { get; set; } = null!;
        public int StockActual { get; set; }
        public int TotalPronosticadoSemestre { get; set; }
        public int UnidadesAComprar { get; set; }
    }
}
