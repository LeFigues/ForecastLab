namespace fl_api.Models.Students
{
    public class StudentDemandItem
    {
        public string Description { get; set; } = null!;
        public string Unit { get; set; } = null!;
        public int TotalGroups { get; set; }
        public int TotalStudents { get; set; }
        public int RequiredQuantity { get; set; }
        public int StockAvailable { get; set; }
        public int MissingQuantity => Math.Max(0, RequiredQuantity - StockAvailable);
        public bool ExistsInSystem { get; set; }
        public int? IdInsumo { get; set; }
    }
}
