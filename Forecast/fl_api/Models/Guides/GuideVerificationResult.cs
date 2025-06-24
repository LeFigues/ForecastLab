using fl_api.Dtos.Guides;

namespace fl_api.Models.Guides
{
    public class GuideVerificationResult
    {
        public List<GuideMaterialVerificationItem> Verificados { get; set; } = new();
        public List<UnmatchedMaterialItem> NoEncontrados { get; set; } = new();
    }
}
