using fl_front.Dtos.OrdersF;

namespace fl_front.Interfaces
{
    public interface IOrderServiceF
    {
        Task<List<PurchaseOrderDtoF>> GetAllAsync();
        Task<PurchaseOrderDetailF?> GetByIdAsync(string id);
        Task<bool> CreateAsync(CreateOrderDtoF dto);
        Task<bool> CambiarEstadoAsync(string id, string accion);
    }
}
