using fl_api.Dtos;

namespace fl_api.Interfaces
{
    public interface IHealthService
    {
        Task<HealthStatusDto> CheckHealthAsync();
    }
}
