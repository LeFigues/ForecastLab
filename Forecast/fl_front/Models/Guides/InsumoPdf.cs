using System.Text.Json.Serialization;

namespace fl_front.Models.Guides
{
    public class InsumoPdf
    {
        [JsonPropertyName("cantidad_por_grupo")]
        public int CantidadPorGrupo { get; set; }

        public string? Unidad { get; set; }
        public string? Descripcion { get; set; }
    }
}
