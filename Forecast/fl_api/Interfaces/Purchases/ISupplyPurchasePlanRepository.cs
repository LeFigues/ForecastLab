using fl_api.Models.Planification;

namespace fl_api.Interfaces.Purchases
{
    public interface ISupplyPurchasePlanRepository
    {
        Task SaveAsync(SupplyPurchasePlan plan);
        Task<List<SupplyPurchasePlan>> GetAllAsync();
        Task<SupplyPurchasePlan?> GetByIdAsync(string id);
    }
}
