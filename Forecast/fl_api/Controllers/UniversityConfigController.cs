using fl_api.Dtos.University;
using fl_api.Interfaces.University;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace fl_api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UniversityConfigController : ControllerBase
    {
        private readonly IUniversityApiConfigService _svc;

        public UniversityConfigController(IUniversityApiConfigService svc)
        {
            _svc = svc;
        }

        [HttpGet]
        public async Task<ActionResult<UniversityApiConfigDto>> Get()
        {
            var config = await _svc.GetConfigAsync();
            return config is null ? NotFound() : Ok(config);
        }

        [HttpPost]
        public async Task<IActionResult> Save(UniversityApiConfigDto dto)
        {
            await _svc.SaveConfigAsync(dto);
            return NoContent();
        }

        [HttpGet("test-connection")]
        public async Task<IActionResult> TestConnection()
        {
            var ok = await _svc.TestConnectionAsync();
            return ok ? Ok("Conexión exitosa") : StatusCode(503, "No se pudo conectar con la API externa");
        }
    }

}
