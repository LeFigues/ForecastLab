using fl_api.Interfaces.Purchases;
using fl_api.Models.Planification;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using OfficeOpenXml;
using System.Text;

namespace fl_api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SupplyPurchasePlanController : ControllerBase
    {
        private readonly ISupplyPurchasePlanService _service;
        private readonly ISupplyPurchasePlanRepository _repository;

        public SupplyPurchasePlanController(
            ISupplyPurchasePlanService service,
            ISupplyPurchasePlanRepository repository)
        {
            _service = service;
            _repository = repository;
        }

        [HttpPost("generate")]
        public async Task<ActionResult<SupplyPurchasePlan>> Generate([FromQuery] string reportId)
        {
            try
            {
                var plan = await _service.GeneratePlanAsync(reportId);
                return Ok(plan);
            }
            catch (Exception ex)
            {
                return BadRequest(new { error = ex.Message });
            }
        }

        [HttpGet]
        public async Task<ActionResult<List<SupplyPurchasePlan>>> GetAll()
        {
            var list = await _repository.GetAllAsync();
            return Ok(list);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<SupplyPurchasePlan>> GetById(string id)
        {
            var plan = await _repository.GetByIdAsync(id);
            if (plan == null) return NotFound();
            return Ok(plan);
        }

        [HttpGet("{id}/export/csv")]
        public async Task<IActionResult> ExportCsv(string id)
        {
            var plan = await _repository.GetByIdAsync(id);
            if (plan == null) return NotFound();

            var sb = new StringBuilder();
            sb.AppendLine("Descripción,Unidad,Cantidad Requerida,Cantidad Recomendada,Precio Estimado,Costo Total");

            foreach (var item in plan.Items)
            {
                var total = item.EstimatedPrice * item.RecommendedQuantity;
                sb.AppendLine($"{item.Description},{item.Unit},{item.RequiredQuantity},{item.RecommendedQuantity},{item.EstimatedPrice},{total}");
            }

            var bytes = Encoding.UTF8.GetBytes(sb.ToString());
            return File(bytes, "text/csv", $"plan_compra_{plan.Faculty}_{plan.Cycle}.csv");
        }

        [HttpGet("{id}/export/excel")]
        public async Task<IActionResult> ExportExcel(string id)
        {
            var plan = await _repository.GetByIdAsync(id);
            if (plan == null) return NotFound();

            using var workbook = new ClosedXML.Excel.XLWorkbook();
            var ws = workbook.Worksheets.Add("Plan de Compras");

            ws.Cell(1, 1).Value = "Descripción";
            ws.Cell(1, 2).Value = "Unidad";
            ws.Cell(1, 3).Value = "Cantidad Requerida";
            ws.Cell(1, 4).Value = "Cantidad Recomendada";
            ws.Cell(1, 5).Value = "Precio Estimado";
            ws.Cell(1, 6).Value = "Costo Total";

            for (int i = 0; i < plan.Items.Count; i++)
            {
                var item = plan.Items[i];
                var row = i + 2;

                ws.Cell(row, 1).Value = item.Description;
                ws.Cell(row, 2).Value = item.Unit;
                ws.Cell(row, 3).Value = item.RequiredQuantity;
                ws.Cell(row, 4).Value = item.RecommendedQuantity;
                ws.Cell(row, 5).Value = item.EstimatedPrice;
                ws.Cell(row, 6).Value = Math.Round(item.EstimatedPrice * item.RecommendedQuantity, 2);
            }

            ws.Columns().AdjustToContents();

            using var stream = new MemoryStream();
            workbook.SaveAs(stream);
            var fileName = $"plan_compra_{plan.Faculty}_{plan.Cycle}.xlsx";
            return File(stream.ToArray(),
                        "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet",
                        fileName);
        }

    }
}
