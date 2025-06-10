namespace fl_front.Models
{
    public class Insumo
    {
        public int Id { get; set; }
        public string Nombre { get; set; } = string.Empty;
        public string Tipo { get; set; } = string.Empty;     // "Equipo", "Insumo", "Reactivo"
        public string Unidad { get; set; } = string.Empty;
        public int Stock { get; set; }
    }
}
