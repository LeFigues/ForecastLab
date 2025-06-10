using fl_api.Dtos.Validation;
using fl_api.Models.Validation;

namespace fl_api.Interfaces.Validation
{
    public interface IForecastValidationService
    {
        //Task<ForecastValidation> ValidateForecastAsync(ForecastValidationRequestDto request);
        Task<List<ForecastValidation>> GetAllAsync();
        Task AddCommentAsync(string validationId, string comment);
    }

}
