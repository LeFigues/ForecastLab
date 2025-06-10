using ClosedXML.Excel;
using fl_api.Dtos.Reports;
using fl_api.Interfaces.Reports;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using OfficeOpenXml;
using System.Text;

namespace fl_api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ReportsController : ControllerBase
    {
        private readonly IReportService _reportService;

        public ReportsController(IReportService reportService)
        {
            _reportService = reportService;
        }

        [HttpPost("consumo-vs-pronostico")]
        public async Task<ActionResult<List<ConsumoVsPronosticoDto>>> GetConsumoVsPronostico([FromBody] ReportFilterDto filter)
        {
            var data = await _reportService.GetConsumoVsPronosticoAsync(filter);
            return Ok(data);
        }

        [HttpPost("unidades-a-comprar")]
        public async Task<ActionResult<List<UnidadesAComprarDto>>> GetUnidadesAComprar([FromBody] ReportFilterDto filter)
        {
            var data = await _reportService.GetUnidadesAComprarAsync(filter);
            return Ok(data);
        }

        [HttpPost("export")]
        public async Task<IActionResult> ExportReport([FromBody] ExportRequestDto request)
        {
            if (request.ReportType == "unidades-a-comprar" && request.Format == "csv")
            {
                var data = await _reportService.GetUnidadesAComprarAsync(request.Filter);

                var csv = new StringBuilder();
                csv.AppendLine("Insumo,Stock actual,Demanda pronosticada,Unidades a comprar,Costo unitario,Costo total");

                foreach (var item in data)
                {
                    csv.AppendLine($"{item.SupplyName},{item.CurrentStock},{item.ForecastedDemand},{item.UnitsToBuy},{item.UnitCost?.ToString("0.00") ?? ""},{item.TotalCost?.ToString("0.00") ?? ""}");
                }

                var fileName = $"unidades-a-comprar-{DateTime.UtcNow:yyyyMMddHHmmss}.csv";
                var bytes = Encoding.UTF8.GetBytes(csv.ToString());
                return File(bytes, "text/csv", fileName);
            }
            else if (request.ReportType == "unidades-a-comprar" && request.Format == "excel")
            {
                var data = await _reportService.GetUnidadesAComprarAsync(request.Filter);

                using var workbook = new XLWorkbook();
                var sheet = workbook.Worksheets.Add("Reporte");

                // Encabezados
                sheet.Cell(1, 1).Value = "Insumo";
                sheet.Cell(1, 2).Value = "Stock actual";
                sheet.Cell(1, 3).Value = "Demanda pronosticada";
                sheet.Cell(1, 4).Value = "Unidades a comprar";
                sheet.Cell(1, 5).Value = "Costo unitario";
                sheet.Cell(1, 6).Value = "Costo total";

                for (int i = 0; i < data.Count; i++)
                {
                    var row = i + 2;
                    var item = data[i];

                    sheet.Cell(row, 1).Value = item.SupplyName;
                    sheet.Cell(row, 2).Value = item.CurrentStock;
                    sheet.Cell(row, 3).Value = item.ForecastedDemand;
                    sheet.Cell(row, 4).Value = item.UnitsToBuy;
                    sheet.Cell(row, 5).Value = item.UnitCost;
                    sheet.Cell(row, 6).Value = item.TotalCost;
                }

                sheet.Columns().AdjustToContents();

                using var stream = new MemoryStream();
                workbook.SaveAs(stream);
                var bytes = stream.ToArray();

                var fileName = $"unidades-a-comprar-{DateTime.UtcNow:yyyyMMddHHmmss}.xlsx";
                return File(bytes, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", fileName);
            }
            else if (request.ReportType == "consumo-vs-pronostico" && request.Format == "csv")
            {
                var data = await _reportService.GetConsumoVsPronosticoAsync(request.Filter);

                var csv = new StringBuilder();
                csv.AppendLine("Mes,Insumo,Consumo real,Pronóstico,Diferencia");

                foreach (var item in data)
                {
                    csv.AppendLine($"{item.Month},{item.SupplyName},{item.RealConsumption},{item.Forecasted},{item.Difference}");
                }

                var fileName = $"consumo-vs-pronostico-{DateTime.UtcNow:yyyyMMddHHmmss}.csv";
                var bytes = Encoding.UTF8.GetBytes(csv.ToString());
                return File(bytes, "text/csv", fileName);
            }
            else if (request.ReportType == "consumo-vs-pronostico" && request.Format == "excel")
            {
                var data = await _reportService.GetConsumoVsPronosticoAsync(request.Filter);

                using var workbook = new XLWorkbook();
                var sheet = workbook.Worksheets.Add("Reporte");

                sheet.Cell(1, 1).Value = "Mes";
                sheet.Cell(1, 2).Value = "Insumo";
                sheet.Cell(1, 3).Value = "Consumo real";
                sheet.Cell(1, 4).Value = "Pronóstico";
                sheet.Cell(1, 5).Value = "Diferencia";

                for (int i = 0; i < data.Count; i++)
                {
                    var row = i + 2;
                    var item = data[i];

                    sheet.Cell(row, 1).Value = item.Month;
                    sheet.Cell(row, 2).Value = item.SupplyName;
                    sheet.Cell(row, 3).Value = item.RealConsumption;
                    sheet.Cell(row, 4).Value = item.Forecasted;
                    sheet.Cell(row, 5).Value = item.Difference;
                }

                sheet.Columns().AdjustToContents();

                using var stream = new MemoryStream();
                workbook.SaveAs(stream);
                var bytes = stream.ToArray();
                var fileName = $"consumo-vs-pronostico-{DateTime.UtcNow:yyyyMMddHHmmss}.xlsx";
                return File(bytes, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", fileName);
            }


            return BadRequest("Formato o tipo de reporte no soportado.");
        }


    }
}
