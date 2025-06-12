namespace fl_front.Dtos.OrdersF
{
    public class PurchaseOrderDtoF
    {
        public string Id { get; set; } = string.Empty;
        public string NumeroOrden { get; set; } = string.Empty;
        public string Proveedor { get; set; } = string.Empty;
        public DateTime FechaEmision { get; set; }
        public string Estado { get; set; } = string.Empty;
        public decimal TotalMonto { get; set; }
    }
}
