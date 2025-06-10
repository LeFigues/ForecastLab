using fl_api.Dtos.University;
using fl_api.Interfaces.University;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace fl_api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UniversityForecastController : ControllerBase
    {
        private readonly IUniversityForecastService _svc;

        public UniversityForecastController(IUniversityForecastService svc)
        {
            _svc = svc;
        }

        [HttpGet("movimientos-por-semestre/{semestreId}")]
        public async Task<ActionResult<List<MovimientoResumenDto>>> GetResumen(string semestreId)
        {
            var data = await _svc.GetMovimientosPorSemestreAsync(semestreId);
            return Ok(data);
        }
        [HttpGet("movimientos-detalle/{insumo}")]
        public async Task<ActionResult<List<MovimientoDetalleDto>>> GetDetalle(string insumo)
        {
            var data = await _svc.GetDetallePorInsumoAsync(insumo);
            return Ok(data);
        }

    }

}
