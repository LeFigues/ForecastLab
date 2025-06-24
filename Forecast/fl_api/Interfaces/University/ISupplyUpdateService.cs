using fl_api.Models.University;

namespace fl_api.Interfaces.University
{
    public interface ISupplyUpdateService
    {
        Task<List<NormalizedSupply>> UpdateAllAsync();
        Task<List<NormalizedSupply>> UpdateByIdsAsync(List<int> ids);
        Task<NormalizedSupply?> UpdateSingleAsync(int idInsumo);
    }
}
