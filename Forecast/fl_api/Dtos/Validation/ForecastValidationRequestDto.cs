namespace fl_api.Dtos.Validation
{
    public class ForecastValidationRequestDto
    {
        public DateTime From { get; set; }
        public DateTime To { get; set; }
        public string? LabId { get; set; }
    }
}
