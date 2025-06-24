using fl_api.Models.Guides;

namespace fl_api_test.Planning
{
    internal class PracticeMaterials : LabMaterials
    {
        public List<MaterialItem> Equipment { get; set; }
        public List<MaterialItem> Supplies { get; set; }
    }
}