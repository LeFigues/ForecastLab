using fl_front.Dtos;

namespace fl_front.Services
{
    public interface IForecastHistoricoService
    {
        Task<ForecastHistoricoSemestreDto[]> GetHistoricoSemestreAsync();
    }
}
