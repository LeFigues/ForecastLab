namespace fl_api.Dtos.University
{
    public class SupplyRawDto
    {
        public int id_insumo { get; set; }
        public string nombre { get; set; } = string.Empty;
        public string descripcion { get; set; } = string.Empty;
        public string ubicacion { get; set; } = string.Empty;
        public string tipo { get; set; } = string.Empty;
        public string unidad_medida { get; set; } = string.Empty;
        public int stock_actual { get; set; }
        public int stock_minimo { get; set; }
        public int stock_maximo { get; set; }
    }
}
