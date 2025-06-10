using fl_api.Dtos;
using fl_api.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace fl_api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class HealthController : ControllerBase
    {
        private readonly IHealthService _health;

        public HealthController(IHealthService health)
        {
            _health = health;
        }

        [HttpGet]
        public async Task<ActionResult<HealthStatusDto>> Get()
        {
            var status = await _health.CheckHealthAsync();
            return Ok(status);
        }
    }
}
