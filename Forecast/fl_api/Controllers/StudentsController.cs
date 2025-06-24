using fl_api.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace fl_api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StudentsController : ControllerBase
    {
        private readonly IStudentsApiService _students;

        public StudentsController(IStudentsApiService students)
        {
            _students = students;
        }

        [HttpGet("health")]
        public async Task<IActionResult> CheckConnection()
        {
            var ok = await _students.CanConnectAsync();
            return Ok(new { connection = ok ? "ok" : "failed" });
        }

        [HttpGet("faculties")]
        public async Task<IActionResult> GetFaculties()
        {
            var result = await _students.GetFacultiesAsync();
            return Ok(result);
        }

        [HttpGet("careers/{facultyId}")]
        public async Task<IActionResult> GetCareersByFaculty(string facultyId)
        {
            var result = await _students.GetCareersByFacultyAsync(facultyId);
            return Ok(result);
        }

        [HttpGet("subjects/{careerId}")]
        public async Task<IActionResult> GetSubjectsByCareer(string careerId)
        {
            var result = await _students.GetSubjectsByCareerAsync(careerId);
            return Ok(result);
        }

        [HttpGet("groups")]
        public async Task<IActionResult> GetFullGroups()
        {
            var result = await _students.GetGroupsFullAsync();
            return Ok(result);
        }

        [HttpGet("cycles")]
        public async Task<IActionResult> GetCycles()
        {
            var result = await _students.GetGroupsFullAsync();
            return Ok(result);
        }
    }
}
