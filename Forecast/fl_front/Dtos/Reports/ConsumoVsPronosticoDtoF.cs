namespace fl_front.Dtos.Reports
{
    public class ConsumoVsPronosticoDtoF
    {
        public string Month { get; set; } = string.Empty;
        public string SupplyName { get; set; } = string.Empty;
        public int RealConsumption { get; set; }
        public int Forecasted { get; set; }
        public int Difference { get; set; }
    }
}
