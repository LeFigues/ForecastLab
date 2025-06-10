using fl_api.Dtos.Planification;
using fl_api.Interfaces.Planification;
using fl_api.Models.Planification;
using MongoDB.Bson;

namespace fl_api.Services.Planification
{
    public class OrderService : IOrderService
    {
        private readonly IPurchaseOrderRepository _repo;

        public OrderService(IPurchaseOrderRepository repo)
        {
            _repo = repo;
        }

        public async Task<PurchaseOrder> CreateAsync(PurchaseOrderCreateDto dto)
        {
            var total = dto.Items.Sum(i => i.Cantidad * i.PrecioUnitario);
            var order = new PurchaseOrder
            {
                Id = ObjectId.GenerateNewId(),
                NumeroOrden = $"ORD-{DateTime.UtcNow.Ticks}",
                Proveedor = dto.Proveedor,
                FechaEmision = DateTime.UtcNow,
                Estado = "Pendiente",
                TotalMonto = total,
                Notas = dto.Notas,
                Items = dto.Items.Select(i => new PurchaseOrderItem
                {
                    Insumo = i.Insumo,
                    Cantidad = i.Cantidad,
                    PrecioUnitario = i.PrecioUnitario,
                    FechaEntregaDeseada = i.FechaEntregaDeseada
                }).ToList()
            };

            await _repo.SaveAsync(order);
            return order;
        }

        public async Task<List<PurchaseOrder>> GetAllAsync()
        {
            return await _repo.GetAllAsync();
        }
        public async Task CambiarEstadoAsync(string id, string nuevoEstado)
        {
            if (!ObjectId.TryParse(id, out var objectId))
                throw new Exception("ID inválido");

            await _repo.UpdateEstadoAsync(objectId, nuevoEstado);
        }
        public async Task<PurchaseOrder?> GetByIdAsync(string id)
        {
            if (!ObjectId.TryParse(id, out var objectId))
                throw new Exception("ID inválido");

            return await _repo.GetByIdAsync(objectId);
        }

    }

}
