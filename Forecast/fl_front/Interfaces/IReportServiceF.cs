using fl_front.Dtos.Reports;

namespace fl_front.Interfaces
{
    public interface IReportServiceF
    {
        Task<List<ConsumoVsPronosticoDtoF>> GetConsumoVsPronosticoAsync(ReportFilterDtoF filter);
        Task<List<UnidadesAComprarDtoF>> GetUnidadesAComprarAsync(ReportFilterDtoF filter);
    }
}
