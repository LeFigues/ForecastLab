using fl_api.Interfaces.University;
using fl_api.Models.University;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace fl_api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SynchronizationController : ControllerBase
    {
        private readonly ISupplyNormalizationService _normalizationService;

        public SynchronizationController(ISupplyNormalizationService normalizationService)
        {
            _normalizationService = normalizationService;
        }

        /// <summary>
        /// Ejecuta la sincronización y normalización de todos los insumos.
        /// </summary>
        [HttpPost("normalize")]
        public async Task<ActionResult<List<NormalizedSupply>>> NormalizeAll()
        {
            var result = await _normalizationService.NormalizeAndSyncAsync();
            return Ok(result);
        }

        /// <summary>
        /// Ejecuta la normalización de un solo insumo por ID.
        /// </summary>
        [HttpPost("normalize/{id}")]
        public async Task<ActionResult<NormalizedSupply>> NormalizeById(int id)
        {
            var result = await _normalizationService.NormalizeSingleAsync(id);
            if (result == null)
                return NotFound($"No se encontró el insumo con ID {id}");
            return Ok(result);
        }

        /// <summary>
        /// Verifica si un insumo aún no ha sido normalizado.
        /// </summary>
        [HttpGet("needs-normalization/{id}")]
        public async Task<ActionResult<bool>> NeedsNormalization(int id)
        {
            var result = await _normalizationService.NeedsNormalizationAsync(id);
            return Ok(result);
        }
    }
}
