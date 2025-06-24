using fl_api.Models.Planification;

namespace fl_api.Interfaces.Planification
{
    public interface ISemesterPurchaseRepository
    {
        Task SaveAsync(SemesterPurchasePlan plan);
        Task<List<SemesterPurchasePlan>> GetAllAsync();
        Task<SemesterPurchasePlan?> GetByIdAsync(string id);
    }
}
