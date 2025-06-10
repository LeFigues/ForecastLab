using fl_api.Dtos.Reports;

namespace fl_api.Interfaces.Reports
{
    public interface IReportService
    {
        Task<List<ConsumoVsPronosticoDto>> GetConsumoVsPronosticoAsync(ReportFilterDto filter);
        Task<List<UnidadesAComprarDto>> GetUnidadesAComprarAsync(ReportFilterDto filter);
        Task<byte[]> ExportReportAsync(ExportRequestDto request);
    }
}
