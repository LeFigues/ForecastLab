namespace fl_front.Dtos.UniversityForecast
{
    public class MovimientoDetalleDtoF
    {
        public DateTime FechaEntregado { get; set; }
        public DateTime? FechaDevuelto { get; set; }
        public int Cantidad { get; set; }
        public string TipoMovimiento { get; set; } = string.Empty;
        public string Responsable { get; set; } = string.Empty;
        public int IdSolicitud { get; set; }
    }
}
