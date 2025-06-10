namespace fl_api.Dtos.University
{
    public class MovimientoResumenDto
    {
        public string Insumo { get; set; } = null!;
        public string TipoMovimiento { get; set; } = null!;
        public string Responsable { get; set; } = null!;
        public int CantidadTotal { get; set; }
    }

}
