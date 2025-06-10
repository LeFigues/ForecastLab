using fl_api.Dtos.Planification;
using fl_api.Interfaces.Planification;
using fl_api.Services.Planification;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace fl_api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class OrdersController : ControllerBase
    {
        private readonly IOrderService _service;

        public OrdersController(IOrderService service)
        {
            _service = service;
        }

        [HttpPost]
        public async Task<IActionResult> Create(PurchaseOrderCreateDto dto)
        {
            var order = await _service.CreateAsync(dto);
            return Ok(order);
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var orders = await _service.GetAllAsync();
            return Ok(orders);
        }
        [HttpPut("{id}/enviar")]
        public async Task<IActionResult> Enviar(string id)
        {
            await _service.CambiarEstadoAsync(id, "Enviado");
            return Ok(new { message = "Orden marcada como Enviada" });
        }

        [HttpPut("{id}/recibir")]
        public async Task<IActionResult> Recibir(string id)
        {
            await _service.CambiarEstadoAsync(id, "Recibido");
            return Ok(new { message = "Orden marcada como Recibida" });
        }
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(string id)
        {
            var order = await _service.GetByIdAsync(id);
            if (order == null)
                return NotFound(new { message = "Orden no encontrada" });

            return Ok(order);
        }

    }

}
