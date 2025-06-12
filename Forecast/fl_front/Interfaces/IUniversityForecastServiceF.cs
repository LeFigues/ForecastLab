using fl_front.Dtos.UniversityForecast;

namespace fl_front.Interfaces
{
    public interface IUniversityForecastServiceF
    {
        Task<List<MovimientoResumenDtoF>> GetResumenPorSemestreAsync(string semestreId);
        Task<List<MovimientoDetalleDtoF>> GetDetallePorInsumoAsync(string insumo);
    }
}
