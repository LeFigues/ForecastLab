using fl_front.Dtos.ForecastF;

namespace fl_front.Interfaces
{
    public interface IForecastServiceF
    {
        Task<List<InsumoPorPracticaDtoF>> GetInsumosPorPracticaAsync();
        Task<List<PracticaUsoDtoF>> GetPracticasUsoAsync();
        Task<List<RiesgoInsumoDtoF>> GetRiesgoInsumosAsync();
        Task<string> AnalizarConIAAsync(List<RiesgoInsumoDtoF> datos);
        Task<List<ForecastPointDtoF>> GetPronosticoPorFechasAsync(DateTime from, DateTime to, string horizon);
        Task<List<HistoricoInsumoDtoF>> GetHistoricoInsumosAsync();
        Task<List<HistoricoPracticaDtoF>> GetHistoricoPracticasAsync();
        Task<List<HistoricoSemestreDtoF>> GetHistoricoSemestreAsync();
        Task<List<RiesgoInsumoDtoF>> GetInsumosEnRiesgoAsync();
        Task<List<ForecastPointDtoF>> GenerarPronosticoAsync(ForecastRequestDtoF request);

    }
}
