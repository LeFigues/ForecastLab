using fl_api.Interfaces.Planification;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace fl_api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PlanningController : ControllerBase
    {
        private readonly IPlanningService _service;
        public PlanningController(IPlanningService service) => _service = service;

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var planes = await _service.GetPlanesAsync();
            return Ok(planes);
        }

        [HttpPost("generar")]
        public async Task<IActionResult> Generar()
        {
            var planes = await _service.GenerarPlanesDeCompraAsync();
            return Ok(planes);
        }
        [HttpGet("calendar")]
        public async Task<IActionResult> Calendar()
        {
            var eventos = await _service.GetCalendarioComprasAsync();
            return Ok(eventos);
        }

    }

}
