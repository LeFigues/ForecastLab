using fl_api.Dtos.Planification;
using fl_api.Models.Planification;

namespace fl_api.Interfaces.Planification
{
    public interface IOrderService
    {
        Task<PurchaseOrder> CreateAsync(PurchaseOrderCreateDto dto);
        Task<List<PurchaseOrder>> GetAllAsync();
        Task CambiarEstadoAsync(string id, string nuevoEstado);
        Task<PurchaseOrder?> GetByIdAsync(string id);

    }
}
