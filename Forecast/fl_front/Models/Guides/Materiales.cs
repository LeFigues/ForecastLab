namespace fl_front.Models.Guides
{
    public class Materiales
    {
        public List<InsumoPdf> Equipos { get; set; } = new();
        public List<InsumoPdf> Insumos { get; set; } = new();
        public List<InsumoPdf> Reactivos { get; set; } = new();
    }
}
