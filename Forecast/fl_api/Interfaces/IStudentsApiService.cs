using fl_api.Dtos.StudentsApi;

namespace fl_api.Interfaces
{
    public interface IStudentsApiService
    {
        Task<bool> CanConnectAsync();
        Task<List<FacultyDto>> GetFacultiesAsync();
        Task<List<CareerDto>> GetCareersByFacultyAsync(string facultyId);
        Task<List<SubjectDto>> GetSubjectsByCareerAsync(string careerId);
        Task<List<GroupDto>> GetGroupsFullAsync();
    }
}
