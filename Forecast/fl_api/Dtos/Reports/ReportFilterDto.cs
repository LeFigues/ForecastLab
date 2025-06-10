namespace fl_api.Dtos.Reports
{
    public class ReportFilterDto
    {
        public DateTime From { get; set; }
        public DateTime To { get; set; }
        public string? LabId { get; set; }           // ID del laboratorio o asignatura
        public string? Category { get; set; }        // Categoría del insumo
    }
}
