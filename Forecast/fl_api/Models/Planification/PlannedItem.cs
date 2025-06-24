namespace fl_api.Models.Planification
{
    public class PlannedItem
    {
        public string Description { get; set; } = null!;
        public string Unit { get; set; } = null!;
        public int RequiredQuantity { get; set; }
        public int StockAvailable { get; set; }
        public int MissingQuantity => Math.Max(0, RequiredQuantity - StockAvailable);
        public bool ExistsInSystem { get; set; }
        public int? IdInsumo { get; set; }
    }
}
