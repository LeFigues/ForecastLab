using fl_front.Dtos.Students;
using University.Dtos;

namespace fl_front.Interfaces.Students
{
    public interface IUniversityStudentServiceF
    {
        Task<List<FacultyDto>> GetFacultiesAsync();
        Task<List<CareerDto>> GetCareersAsync();
        Task<List<CycleDto>> GetCyclesAsync();
        Task<List<SubjectDto>> GetSubjectsAsync();
    }
}
