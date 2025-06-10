namespace fl_api.Dtos.Reports
{
    public class ExportRequestDto
    {
        public ReportFilterDto Filter { get; set; } = null!;
        public string ReportType { get; set; } = null!; // "consumo-vs-pronostico", "unidades-a-comprar"
        public string Format { get; set; } = null!;     // "pdf", "excel", "csv"
    }
}
