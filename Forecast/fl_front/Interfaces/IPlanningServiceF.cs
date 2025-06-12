using fl_front.Dtos.Planification;

namespace fl_front.Interfaces
{
    public interface IPlanningServiceF
    {
        Task<List<PlanCompraDtoF>> GetPlanesAsync();
        Task<List<EventoCompraDtoF>> GetCalendarioEventosAsync();
        Task<bool> GenerarPlanesAsync();
    }
}
