using fl_api.Dtos.Planification;
using fl_api.Models.Forecast;
using fl_api.Models.Planification;

namespace fl_api.Interfaces.Planification
{
    public interface IPlanningService
    {
        Task<List<PurchasePlan>> GenerarPlanesDeCompraAsync();
        Task<List<PurchasePlan>> GetPlanesAsync();
        Task<List<CalendarEventDto>> GetCalendarioComprasAsync();

    }
}
