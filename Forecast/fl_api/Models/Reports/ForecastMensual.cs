namespace fl_api.Models.Reports
{
    public class ForecastMensual
    {
        public string InsumoNombre { get; set; } = null!;
        public DateTime PeriodStart { get; set; }
        public int ForecastedQuantity { get; set; }
    }

}
