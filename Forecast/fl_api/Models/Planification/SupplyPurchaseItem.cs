namespace fl_api.Models.Planification
{
    public class SupplyPurchaseItem
    {
        public string Description { get; set; } = string.Empty;
        public string Unit { get; set; } = string.Empty;
        public int RequiredQuantity { get; set; }
        public int RecommendedQuantity { get; set; }
        public decimal EstimatedPrice { get; set; }
        public decimal TotalCost => Math.Round(EstimatedPrice * RecommendedQuantity, 2);
        public bool ExistsInSystem { get; set; }
        public int? IdInsumo { get; set; }
    }
}
