namespace fl_api.Dtos.University
{
    public class MovimientoDetalleDto
    {
        public DateTime FechaEntregado { get; set; }
        public DateTime? FechaDevuelto { get; set; }
        public int Cantidad { get; set; }
        public string TipoMovimiento { get; set; } = null!;
        public string Responsable { get; set; } = null!;
        public int? IdSolicitud { get; set; } // ← aquí el cambio
    }
}
