namespace fl_front.Dtos.OrdersF
{
    public class ItemDtoF
    {
        public string Insumo { get; set; } = string.Empty;
        public int Cantidad { get; set; }
        public decimal PrecioUnitario { get; set; }
        public DateTime FechaEntregaDeseada { get; set; } = DateTime.Today;
    }
}
