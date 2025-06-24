using MongoDB.Bson.Serialization.Attributes;
using Newtonsoft.Json;

namespace fl_api.Models.Guides
{
    public class LabMaterials
    {
        [JsonProperty("equipos")]
        public List<LabItem> Equipment { get; set; } = new();

        [JsonProperty("insumos")]
        public List<LabItem> Supplies { get; set; } = new();
    }
}
