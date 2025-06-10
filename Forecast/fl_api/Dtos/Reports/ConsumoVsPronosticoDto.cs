namespace fl_api.Dtos.Reports
{
    public class ConsumoVsPronosticoDto
    {
        public string Month { get; set; } = null!;
        public string SupplyName { get; set; } = null!;
        public int RealConsumption { get; set; }
        public int Forecasted { get; set; }
        public int Difference => RealConsumption - Forecasted;
    }
}
