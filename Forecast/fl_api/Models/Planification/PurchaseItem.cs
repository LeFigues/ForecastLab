namespace fl_api.Models.Planification
{
    public class PurchaseItem
    {
        public string Nombre { get; set; }
        public int CantidadRequerida { get; set; }
        public int StockActual { get; set; }
        public string Unidad { get; set; }
        public int? IdInsumo { get; set; }
        public int Faltante => Math.Max(0, CantidadRequerida - StockActual);
    }
}
