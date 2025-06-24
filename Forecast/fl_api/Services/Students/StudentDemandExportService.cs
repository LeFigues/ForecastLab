using ClosedXML.Excel;
using fl_api.Interfaces.Students;
using OfficeOpenXml;
using System.Text;

namespace fl_api.Services.Students
{
    public class StudentDemandExportService : IStudentDemandExportService
    {
        private readonly IStudentDemandForecastRepository _repo;

        public StudentDemandExportService(IStudentDemandForecastRepository repo)
        {
            _repo = repo;
        }

        public async Task<byte[]> ExportCsvAsync(string id)
        {
            var report = await _repo.GetByIdAsync(id)
                         ?? throw new Exception("Report not found");

            var lines = new List<string>
            {
                "Descripcion,Unidad,Cantidad Requerida,Stock,Disponible,Existente en Sistema,ID Insumo"
            };

            lines.AddRange(report.Items.Select(i =>
                $"{i.Description},{i.Unit},{i.RequiredQuantity},{i.StockAvailable},{i.MissingQuantity},{i.ExistsInSystem},{i.IdInsumo}"));

            return System.Text.Encoding.UTF8.GetBytes(string.Join("\n", lines));
        }

        public async Task<byte[]> ExportExcelAsync(string id)
        {
            var report = await _repo.GetByIdAsync(id)
                         ?? throw new Exception("Report not found");

            using var workbook = new XLWorkbook();
            var worksheet = workbook.Worksheets.Add("Reporte");

            // Header
            worksheet.Cell(1, 1).Value = "Descripción";
            worksheet.Cell(1, 2).Value = "Unidad";
            worksheet.Cell(1, 3).Value = "Cantidad Requerida";
            worksheet.Cell(1, 4).Value = "Stock Disponible";
            worksheet.Cell(1, 5).Value = "Faltante";
            worksheet.Cell(1, 6).Value = "En Sistema";
            worksheet.Cell(1, 7).Value = "ID Insumo";

            // Data
            for (int i = 0; i < report.Items.Count; i++)
            {
                var item = report.Items[i];
                worksheet.Cell(i + 2, 1).Value = item.Description;
                worksheet.Cell(i + 2, 2).Value = item.Unit;
                worksheet.Cell(i + 2, 3).Value = item.RequiredQuantity;
                worksheet.Cell(i + 2, 4).Value = item.StockAvailable;
                worksheet.Cell(i + 2, 5).Value = item.MissingQuantity;
                worksheet.Cell(i + 2, 6).Value = item.ExistsInSystem ? "Sí" : "No";
                worksheet.Cell(i + 2, 7).Value = item.IdInsumo?.ToString() ?? "-";
            }

            using var stream = new MemoryStream();
            workbook.SaveAs(stream);
            return stream.ToArray();
        }
    }
}
