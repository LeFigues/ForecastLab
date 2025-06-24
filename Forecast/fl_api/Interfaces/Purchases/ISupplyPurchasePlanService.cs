using fl_api.Models.Planification;

namespace fl_api.Interfaces.Purchases
{
    public interface ISupplyPurchasePlanService
    {
        Task<SupplyPurchasePlan> GeneratePlanAsync(string reportId);
    }
}
