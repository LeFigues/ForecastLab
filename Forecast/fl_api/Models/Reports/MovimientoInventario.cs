namespace fl_api.Models.Reports
{
    public class MovimientoInventario
    {
        public int IdMovimiento { get; set; }
        public string TipoMovimiento { get; set; } = null!;
        public DateTime FechaEntregado { get; set; }
        public DateTime? FechaDevuelto { get; set; }
        public int Cantidad { get; set; }
        public string Responsable { get; set; } = null!;
        public int IdSolicitud { get; set; }
        public string InsumoNombre { get; set; } = null!;
    }

}
