using fl_api.Models.University;

namespace fl_api.Interfaces.University
{
    public interface ISupplyNormalizationService
    {
        Task<List<NormalizedSupply>> NormalizeAndSyncAsync(bool forceRefresh = false);
        Task<NormalizedSupply?> NormalizeSingleAsync(int idInsumo);
        Task<bool> NeedsNormalizationAsync(int idInsumo);
    }
}
