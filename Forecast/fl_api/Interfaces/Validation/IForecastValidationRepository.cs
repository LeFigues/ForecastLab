using fl_api.Models.Validation;

namespace fl_api.Interfaces.Validation
{
    public interface IForecastValidationRepository
    {
        Task SaveAsync(ForecastValidation validation);
        Task<List<ForecastValidation>> GetAllAsync();
        Task<ForecastValidation?> GetByIdAsync(string id);
        Task AddCommentAsync(string id, string comment);
    }

}
