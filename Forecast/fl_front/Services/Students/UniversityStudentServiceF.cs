using fl_front.Dtos.Students;
using fl_front.Interfaces.Students;
using System.Net.Http.Json;

namespace fl_front.Services.Students
{
    public class UniversityStudentServiceF : IUniversityStudentServiceF
    {
        private readonly HttpClient _http;

        public UniversityStudentServiceF(HttpClient http) => _http = http;

        public async Task<List<FacultyDto>> GetFacultiesAsync() =>
            await _http.GetFromJsonAsync<List<FacultyDto>>("api/faculty") ?? [];

        public async Task<List<CareerDto>> GetCareersAsync() =>
            await _http.GetFromJsonAsync<List<CareerDto>>("api/career") ?? [];

        public async Task<List<CycleDto>> GetCyclesAsync() =>
            await _http.GetFromJsonAsync<List<CycleDto>>("api/cycle") ?? [];

        public async Task<List<SubjectDto>> GetSubjectsAsync() =>
            await _http.GetFromJsonAsync<List<SubjectDto>>("api/subject") ?? [];
    }
}
