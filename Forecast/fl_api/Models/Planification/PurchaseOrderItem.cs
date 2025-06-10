namespace fl_api.Models.Planification
{
    public class PurchaseOrderItem
    {
        public string Insumo { get; set; } = null!;
        public int Cantidad { get; set; }
        public decimal PrecioUnitario { get; set; }
        public DateTime FechaEntregaDeseada { get; set; }
    }

}
