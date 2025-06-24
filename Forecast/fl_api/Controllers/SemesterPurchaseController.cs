using fl_api.Interfaces.Planification;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace fl_api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SemesterPurchaseController : ControllerBase
    {
        private readonly ISemesterPurchaseService _service;

        public SemesterPurchaseController(ISemesterPurchaseService service)
        {
            _service = service;
        }

        // POST: /api/semesterpurchase/generate?ciclo=2025-2&facultad=Informatica
        [HttpPost("generate")]
        public async Task<IActionResult> GeneratePlan([FromQuery] string ciclo, [FromQuery] string facultad)
        {
            var plan = await _service.GeneratePlanAsync(ciclo, facultad);
            return Ok(plan);
        }

        // GET: /api/semesterpurchase
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var plans = await _service.GetAllPlansAsync();
            return Ok(plans);
        }

        // GET: /api/semesterpurchase/{id}
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(string id)
        {
            var plan = await _service.GetPlanByIdAsync(id);
            if (plan == null)
                return NotFound();

            return Ok(plan);
        }

        [HttpGet("{id}/csv")]
        public async Task<IActionResult> ExportToCsv(string id)
        {
            var plan = await _service.GetPlanByIdAsync(id);
            if (plan == null)
                return NotFound("Plan no encontrado.");

            var lines = new List<string>
    {
        "Descripción,Unidad,Cantidad Requerida,Stock Disponible,Faltante,Existe en Sistema,Id Insumo"
    };

            foreach (var item in plan.Items)
            {
                var line = $"\"{item.Description}\",{item.Unit},{item.RequiredQuantity},{item.StockAvailable},{item.MissingQuantity},{item.ExistsInSystem},{item.IdInsumo?.ToString() ?? ""}";
                lines.Add(line);
            }

            var csvContent = string.Join("\n", lines);
            var bytes = System.Text.Encoding.UTF8.GetBytes(csvContent);
            return File(bytes, "text/csv", $"plan_{plan.Faculty}_{plan.Cycle}.csv");
        }
        [HttpGet("{id}/excel")]
        public async Task<IActionResult> ExportToExcel(string id)
        {
            var plan = await _service.GetPlanByIdAsync(id);
            if (plan == null)
                return NotFound("Plan no encontrado.");

            using var workbook = new ClosedXML.Excel.XLWorkbook();
            var worksheet = workbook.Worksheets.Add("Plan de Compras");

            // Encabezados
            worksheet.Cell(1, 1).Value = "Descripción";
            worksheet.Cell(1, 2).Value = "Unidad";
            worksheet.Cell(1, 3).Value = "Cantidad Requerida";
            worksheet.Cell(1, 4).Value = "Stock Disponible";
            worksheet.Cell(1, 5).Value = "Faltante";
            worksheet.Cell(1, 6).Value = "Existe en Sistema";
            worksheet.Cell(1, 7).Value = "Id Insumo";

            // Datos
            for (int i = 0; i < plan.Items.Count; i++)
            {
                var item = plan.Items[i];
                worksheet.Cell(i + 2, 1).Value = item.Description;
                worksheet.Cell(i + 2, 2).Value = item.Unit;
                worksheet.Cell(i + 2, 3).Value = item.RequiredQuantity;
                worksheet.Cell(i + 2, 4).Value = item.StockAvailable;
                worksheet.Cell(i + 2, 5).Value = item.MissingQuantity;
                worksheet.Cell(i + 2, 6).Value = item.ExistsInSystem ? "Sí" : "No";
                worksheet.Cell(i + 2, 7).Value = item.IdInsumo?.ToString() ?? "";
            }

            using var stream = new MemoryStream();
            workbook.SaveAs(stream);
            stream.Seek(0, SeekOrigin.Begin);

            return File(stream.ToArray(), "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", $"plan_{plan.Faculty}_{plan.Cycle}.xlsx");
        }

    }
}
