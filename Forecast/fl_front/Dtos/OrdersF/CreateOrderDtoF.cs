using fl_front.Dtos.Planification;

namespace fl_front.Dtos.OrdersF
{
    public class CreateOrderDtoF
    {
        public string Proveedor { get; set; } = string.Empty;
        public string Notas { get; set; } = string.Empty;
        public List<PurchaseOrderItemDtoF> Items { get; set; } = new();
    }

}
