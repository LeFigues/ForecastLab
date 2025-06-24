using MongoDB.Bson.Serialization.Attributes;
using Newtonsoft.Json;

namespace fl_api.Models.Guides
{
    public class LabPractice
    {
        [JsonProperty("titulo")]
        public string Title { get; set; } = null!;

        [JsonProperty("cantidad_grupos")]
        public int GroupCount { get; set; }

        [JsonProperty("estudiantes_por_grupo")]
        public int StudentsPerGroup { get; set; }

        [JsonProperty("materiales")]
        public LabMaterials Materials { get; set; } = new();
        

    }
}
