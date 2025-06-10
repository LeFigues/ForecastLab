namespace fl_api.Dtos.Validation
{
    public class ForecastValidationCommentDto
    {
        public string ValidationId { get; set; }
        public string Comment { get; set; } = null!;
    }
}
