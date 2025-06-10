namespace fl_api.Dtos.Forecast
{
    public class ItemDto
    {
        public string Description { get; set; } = null!;
        public string Unit { get; set; } = null!;
        public int TotalQuantity { get; set; }
    }
}
