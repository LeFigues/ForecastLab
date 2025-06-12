namespace fl_front.Dtos.Reports
{
    public class UnidadesAComprarDtoF
    {
        public string SupplyName { get; set; } = string.Empty;
        public int CurrentStock { get; set; }
        public int ForecastedDemand { get; set; }
        public int UnitsToBuy { get; set; }
        public decimal? UnitCost { get; set; }
        public decimal? TotalCost { get; set; }
    }
}
