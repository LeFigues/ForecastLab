using fl_front.Dtos.Health;

namespace fl_front.Interfaces
{
    public interface IHealthService
    {
        Task<HealthStatusDto?> GetHealthStatusAsync();
    }

}
