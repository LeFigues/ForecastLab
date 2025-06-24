namespace fl_api.Models.Guides
{
    public class GuideMaterialVerificationItem
    {
        public string Nombre { get; set; } = string.Empty;
        public int CantidadTotal { get; set; }
        public int Stock { get; set; }
        public int Faltante => Math.Max(CantidadTotal - Stock, 0);
        public string Estado => Faltante > 0 ? "Faltante" : "Suficiente";
        public int? IdInsumo { get; set; }
        public string? Unidad { get; set; }

        public bool EsSugerencia { get; set; } = false;
    }
}
