using fl_api.Dtos.StudentsApi;
using fl_api.Interfaces;

namespace fl_api.Services
{
    public class StudentsApiService : IStudentsApiService
    {
        private readonly HttpClient _http;

        public StudentsApiService(HttpClient http) => _http = http;

        public async Task<bool> CanConnectAsync()
        {
            try
            {
                using var resp = await _http.GetAsync("api/health");
                return resp.IsSuccessStatusCode;
            }
            catch
            {
                return false;
            }
        }

        public async Task<List<FacultyDto>> GetFacultiesAsync()
        {
            return await _http.GetFromJsonAsync<List<FacultyDto>>("api/faculty") ?? new();
        }

        public async Task<List<CareerDto>> GetCareersByFacultyAsync(string facultyId)
        {
            return await _http.GetFromJsonAsync<List<CareerDto>>($"api/career/by-faculty/{facultyId}") ?? new();
        }

        public async Task<List<SubjectDto>> GetSubjectsByCareerAsync(string careerId)
        {
            return await _http.GetFromJsonAsync<List<SubjectDto>>($"api/subject/by-career/{careerId}") ?? new();
        }

        public async Task<List<GroupDto>> GetGroupsFullAsync()
        {
            return await _http.GetFromJsonAsync<List<GroupDto>>("api/group/full") ?? new();
        }

        public async Task<List<GroupDto>> GetCycles()
        {
            return await _http.GetFromJsonAsync<List<GroupDto>>("api/cycles/full") ?? new();
        }
    }

}
