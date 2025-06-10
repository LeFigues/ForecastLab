using fl_api.Dtos.Validation;
using fl_api.Interfaces.Validation;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace fl_api.Controllers
{
    [ApiController]
    [Route("api/forecast-validation")]
    public class ForecastValidationController : ControllerBase
    {
        private readonly IForecastValidationService _service;

        public ForecastValidationController(IForecastValidationService service)
            => _service = service;

        //[HttpPost("compare")]
        //public async Task<IActionResult> Validate([FromBody] ForecastValidationRequestDto request)
        //{
        //    var result = await _service.ValidateForecastAsync(request);
        //    return Ok(result);
        //}

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var list = await _service.GetAllAsync();
            return Ok(list);
        }

        [HttpPost("comment")]
        public async Task<IActionResult> AddComment([FromBody] ForecastValidationCommentDto dto)
        {
            await _service.AddCommentAsync(dto.ValidationId, dto.Comment);
            return NoContent();
        }
    }

}
