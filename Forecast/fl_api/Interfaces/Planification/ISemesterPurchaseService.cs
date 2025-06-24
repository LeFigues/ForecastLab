using fl_api.Models.Planification;

namespace fl_api.Interfaces.Planification
{
    public interface ISemesterPurchaseService
    {
        Task<SemesterPurchasePlan> GeneratePlanAsync(string ciclo, string facultad);
        Task<List<SemesterPurchasePlan>> GetAllPlansAsync();
        Task<SemesterPurchasePlan?> GetPlanByIdAsync(string id);
    }
}
