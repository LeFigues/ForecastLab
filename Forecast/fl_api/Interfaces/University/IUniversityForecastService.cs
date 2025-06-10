using fl_api.Dtos.University;

namespace fl_api.Interfaces.University
{
    public interface IUniversityForecastService
    {
        Task<List<MovimientoResumenDto>> GetMovimientosPorSemestreAsync(string semestreId);
        Task<List<MovimientoDetalleDto>> GetDetallePorInsumoAsync(string insumoNombre);
        Task<List<MovimientoResumenDto>> GetForecastPorRangoAsync(DateTime from, DateTime to);
        Task<List<ForecastSemestralDto>> GetForecastSemestralAsync();
    }

}
