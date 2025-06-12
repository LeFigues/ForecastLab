namespace fl_front.Dtos.OrdersF
{
    public class PurchaseOrderDetailF
    {
        public string Id { get; set; } = string.Empty;
        public string NumeroOrden { get; set; } = string.Empty;
        public string Proveedor { get; set; } = string.Empty;
        public DateTime FechaEmision { get; set; }
        public string Estado { get; set; } = string.Empty;
        public decimal TotalMonto { get; set; }
        public string Notas { get; set; } = string.Empty;
        public List<ItemDtoF> Items { get; set; } = new();
    }
}
