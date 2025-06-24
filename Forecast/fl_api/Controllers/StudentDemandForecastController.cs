using fl_api.Interfaces.Students;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace fl_api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StudentDemandForecastController : ControllerBase
    {
        private readonly IStudentDemandForecastService _service;
        private readonly IStudentDemandExportService _export;

        public StudentDemandForecastController(IStudentDemandForecastService service, IStudentDemandExportService export)
        {
            _service = service;
            _export = export;
        }

        // POST: /api/studentdemandforecast/generate?ciclo=2025-2&facultad=Informatica
        [HttpPost("generate")]
        public async Task<IActionResult> Generate([FromQuery] string ciclo, [FromQuery] string facultad)
        {
            var result = await _service.GenerateAsync(ciclo, facultad);
            return Ok(result);
        }

        // GET: /api/studentdemandforecast
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var result = await _service.GetAllAsync();
            return Ok(result);
        }

        // GET: /api/studentdemandforecast/{id}
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(string id)
        {
            var result = await _service.GetByIdAsync(id);
            if (result == null) return NotFound();
            return Ok(result);
        }

        [HttpGet("{id}/export/csv")]
        public async Task<IActionResult> ExportCsv(string id)
        {
            var bytes = await _export.ExportCsvAsync(id);
            return File(bytes, "text/csv", $"student-demand-{id}.csv");
        }

        [HttpGet("{id}/export/excel")]
        public async Task<IActionResult> ExportExcel(string id)
        {
            var bytes = await _export.ExportExcelAsync(id);
            return File(bytes, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", $"student-demand-{id}.xlsx");
        }

    }
}
