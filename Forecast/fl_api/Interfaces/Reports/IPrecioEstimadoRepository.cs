using fl_api.Models.Reports;

namespace fl_api.Interfaces.Reports
{
    public interface IPrecioEstimadoRepository
    {
        Task<List<PrecioEstimadoRecord>> GetAllAsync();
        Task<PrecioEstimadoRecord?> GetByNombreAsync(string nombre);
        Task SaveManyAsync(IEnumerable<PrecioEstimadoRecord> precios);
    }
}
