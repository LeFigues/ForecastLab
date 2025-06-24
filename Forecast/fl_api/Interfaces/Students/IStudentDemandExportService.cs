namespace fl_api.Interfaces.Students
{
    public interface IStudentDemandExportService
    {
        Task<byte[]> ExportCsvAsync(string reportId);
        Task<byte[]> ExportExcelAsync(string reportId);
    }
}
