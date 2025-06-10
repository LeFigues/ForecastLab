namespace fl_api.Dtos.Forecast
{
    public class ForecastRequestADto
    {
        public DateTime From { get; set; }
        public DateTime To { get; set; }

        /// <summary>
        /// "monthly" o "semestral"
        /// </summary>
        public string Horizon { get; set; } = "monthly";
    }
}
