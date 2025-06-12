namespace fl_front.Dtos.ForecastF
{
    public class ForecastRequestDtoF
    {
        public string From { get; set; } = string.Empty;
        public string To { get; set; } = string.Empty;
        public string Horizon { get; set; } = "monthly";
    }
}
