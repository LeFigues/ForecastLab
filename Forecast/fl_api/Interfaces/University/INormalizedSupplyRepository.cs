using fl_api.Models.University;

namespace fl_api.Interfaces.University
{
    public interface INormalizedSupplyRepository
    {
        Task<List<NormalizedSupply>> GetAllAsync();
        Task<NormalizedSupply?> GetByIdInsumoAsync(int idInsumo);
        Task InsertManyAsync(List<NormalizedSupply> supplies);
        Task UpsertAsync(NormalizedSupply supply);
        Task DeleteAllAsync();
    }
}
