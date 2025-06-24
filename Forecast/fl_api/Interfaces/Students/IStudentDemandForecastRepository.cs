using fl_api.Models.Students;

namespace fl_api.Interfaces.Students
{
    public interface IStudentDemandForecastRepository
    {
        Task SaveAsync(StudentDemandReport report);
        Task<List<StudentDemandReport>> GetAllAsync();
        Task<StudentDemandReport?> GetByIdAsync(string id);
    }
}
