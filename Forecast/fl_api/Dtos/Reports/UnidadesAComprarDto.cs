namespace fl_api.Dtos.Reports
{
    public class UnidadesAComprarDto
    {
        public string SupplyName { get; set; } = null!;
        public int CurrentStock { get; set; }
        public int ForecastedDemand { get; set; }
        public int UnitsToBuy => Math.Max(ForecastedDemand - CurrentStock, 0);
        public decimal? UnitCost { get; set; }
        public decimal? TotalCost => UnitCost.HasValue ? UnitCost.Value * UnitsToBuy : null;
    }
}
