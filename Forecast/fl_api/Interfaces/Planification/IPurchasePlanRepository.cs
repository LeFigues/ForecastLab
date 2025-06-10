using fl_api.Models.Planification;

namespace fl_api.Interfaces.Planification
{
    public interface IPurchasePlanRepository
    {
        Task SaveManyAsync(IEnumerable<PurchasePlan> planes);
        Task<List<PurchasePlan>> GetAllAsync();
        Task<List<string>> GetInsumosPlanificadosAsync();

    }
}
