using MongoDB.Bson.Serialization.Attributes;
using Newtonsoft.Json;

namespace fl_api.Models.Guides
{
    public class LabItem
    {
        [JsonProperty("cantidad_por_grupo")]
        public int QuantityPerGroup { get; set; }

        [JsonProperty("unidad")]
        public string Unit { get; set; } = null!;

        [JsonProperty("descripcion")]
        public string Description { get; set; } = null!;
    }
}
