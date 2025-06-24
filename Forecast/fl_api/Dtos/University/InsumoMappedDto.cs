namespace fl_api.Dtos.University
{
    public class InsumoMappedDto
    {
        public int IdInsumo { get; set; }
        public string Nombre { get; set; } = string.Empty;
        public string Descripcion { get; set; } = string.Empty;
        public string Tipo { get; set; } = string.Empty;
        public string UnidadMedida { get; set; } = string.Empty;
        public int StockTotal { get; set; }
        public int StockMinimo { get; set; }
        public int StockMaximo { get; set; }
    }
}
