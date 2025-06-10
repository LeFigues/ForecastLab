using fl_api.Models.Planification;
using MongoDB.Bson;

namespace fl_api.Interfaces.Planification
{
    public interface IPurchaseOrderRepository
    {
        Task SaveAsync(PurchaseOrder order);
        Task<List<PurchaseOrder>> GetAllAsync();
        Task UpdateEstadoAsync(ObjectId id, string nuevoEstado);
        Task<PurchaseOrder?> GetByIdAsync(ObjectId id);

    }

}
