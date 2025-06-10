namespace fl_api.Dtos.Planification
{
    public class PurchaseOrderItemDto
    {
        public string Insumo { get; set; } = null!;
        public int Cantidad { get; set; }
        public decimal PrecioUnitario { get; set; }
        public DateTime FechaEntregaDeseada { get; set; }
    }
}
