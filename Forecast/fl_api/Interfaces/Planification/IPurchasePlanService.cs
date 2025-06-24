using fl_api.Models.Planification;

namespace fl_api.Interfaces.Planification
{
    public interface IPurchasePlanService
    {
        Task<PurchasePlan> GeneratePlanAsync(string ciclo, string facultad);
        Task<List<PurchasePlan>> GetAllAsync();
        Task<PurchasePlan?> GetByIdAsync(string id);
    }
}
