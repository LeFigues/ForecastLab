namespace fl_api.Dtos.Planification
{
    public class CalendarEventDto
    {
        public string Title { get; set; } = null!;
        public string Date { get; set; } = null!; // ISO date only
        public string Color { get; set; } = "yellow"; // por defecto
    }
}
