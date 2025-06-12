namespace fl_front.Dtos.Planification
{
    public class PlanCompraDtoF
    {
        public string Insumo { get; set; } = string.Empty;
        public int UnidadesAComprar { get; set; }
        public DateTime FechaSugeridaCompra { get; set; }
        public string ProveedorSugerido { get; set; } = string.Empty;
        public string Estado { get; set; } = string.Empty;
    }
}
