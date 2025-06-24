using fl_api.Interfaces.University;
using fl_api.Models.University;
using Microsoft.AspNetCore.Mvc;

namespace fl_api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class NormalizedSuppliesController : ControllerBase
    {
        private readonly INormalizedSupplyRepository _repository;
        private readonly ISupplyUpdateService _updateService;

        public NormalizedSuppliesController(
            INormalizedSupplyRepository repository,
            ISupplyUpdateService updateService)
        {
            _repository = repository;
            _updateService = updateService;
        }

        // Obtener todos
        [HttpGet]
        public async Task<ActionResult<List<NormalizedSupply>>> GetAll()
        {
            var result = await _repository.GetAllAsync();
            return Ok(result);
        }

        // Buscar por IdInsumo
        [HttpGet("{id}")]
        public async Task<ActionResult<NormalizedSupply>> GetById(int id)
        {
            var result = await _repository.GetByIdInsumoAsync(id);
            if (result == null)
                return NotFound($"No se encontró el insumo con IdInsumo = {id}");
            return Ok(result);
        }

        // Buscar por nombre (contiene)
        [HttpGet("buscar")]
        public async Task<ActionResult<List<NormalizedSupply>>> BuscarPorNombre([FromQuery] string nombre)
        {
            var all = await _repository.GetAllAsync();
            var result = all
                .Where(x => x.Nombre.Contains(nombre, StringComparison.OrdinalIgnoreCase))
                .ToList();

            return Ok(result);
        }

        // Actualizar todos los precios y vida útil usando IA
        [HttpPost("actualizar-todo")]
        public async Task<ActionResult<List<NormalizedSupply>>> ActualizarTodo()
        {
            var result = await _updateService.UpdateAllAsync();
            return Ok(result);
        }

        // Actualizar varios por IdInsumo
        [HttpPost("actualizar-seleccionados")]
        public async Task<ActionResult<List<NormalizedSupply>>> ActualizarSeleccionados([FromBody] List<int> ids)
        {
            if (ids == null || !ids.Any())
                return BadRequest("Debe proporcionar al menos un IdInsumo.");

            var result = await _updateService.UpdateByIdsAsync(ids);
            return Ok(result);
        }

        // Actualizar uno por IdInsumo
        [HttpPost("actualizar/{id}")]
        public async Task<ActionResult<NormalizedSupply>> ActualizarUno(int id)
        {
            var result = await _updateService.UpdateSingleAsync(id);
            if (result == null)
                return NotFound($"No se encontró el insumo con IdInsumo = {id}");
            return Ok(result);
        }
    }
}
