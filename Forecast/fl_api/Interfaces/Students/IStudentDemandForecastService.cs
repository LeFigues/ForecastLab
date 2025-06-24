using fl_api.Models.Students;

namespace fl_api.Interfaces.Students
{
    public interface IStudentDemandForecastService
    {
        Task<StudentDemandReport> GenerateAsync(string ciclo, string facultad);
        Task<List<StudentDemandReport>> GetAllAsync();
        Task<StudentDemandReport?> GetByIdAsync(string id);
    }
}
