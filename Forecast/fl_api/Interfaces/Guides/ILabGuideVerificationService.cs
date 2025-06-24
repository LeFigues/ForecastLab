using fl_api.Models.Guides;

namespace fl_api.Interfaces.Guides
{
    public interface ILabGuideVerificationService
    {
        Task<GuideVerificationResult> VerifyAgainstStockAsync(LabGuide guide);
    }
}
