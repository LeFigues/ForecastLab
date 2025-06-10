namespace fl_front.Models.Guides
{
    public class PracticaItem
    {
        public int PracticaNumero { get; set; }
        public string? Titulo { get; set; }
        public Materiales Materiales { get; set; } = new();
    }
}
