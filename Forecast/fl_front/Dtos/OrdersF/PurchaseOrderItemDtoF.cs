namespace fl_front.Dtos.Planification
{
    public class PurchaseOrderItemDtoF
    {
        public string Insumo { get; set; } = null!;
        public int Cantidad { get; set; }
        public decimal PrecioUnitario { get; set; }
        public DateTime FechaEntregaDeseada { get; set; }
    }
}
