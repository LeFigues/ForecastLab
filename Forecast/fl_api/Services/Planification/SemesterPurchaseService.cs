using fl_api.Interfaces.Guides;
using fl_api.Interfaces.Planification;
using fl_api.Interfaces.University;
using fl_api.Models.Planification;

namespace fl_api.Services.Planification
{
    public class SemesterPurchaseService : ISemesterPurchaseService
    {
        private readonly ILabGuideRepository _labGuideRepo;
        private readonly INormalizedSupplyRepository _supplyRepo;
        private readonly ISemesterPurchaseRepository _planRepo;

        public SemesterPurchaseService(
            ILabGuideRepository labGuideRepo,
            INormalizedSupplyRepository supplyRepo,
            ISemesterPurchaseRepository planRepo)
        {
            _labGuideRepo = labGuideRepo;
            _supplyRepo = supplyRepo;
            _planRepo = planRepo;
        }

        public async Task<SemesterPurchasePlan> GeneratePlanAsync(string ciclo, string facultad)
        {
            var guides = await _labGuideRepo.GetByCycleAndFacultyAsync(ciclo, facultad);
            var supplies = await _supplyRepo.GetAllAsync();

            var aggregate = new Dictionary<string, (int Total, string Unidad)>(StringComparer.OrdinalIgnoreCase);

            foreach (var guide in guides)
            {
                foreach (var practice in guide.Practices)
                {
                    var grupos = practice.GroupCount;
                    foreach (var item in practice.Materials.Equipment.Concat(practice.Materials.Supplies))
                    {
                        var clave = item.Description.Trim().ToLower();
                        var cantidad = item.QuantityPerGroup * grupos;

                        if (!aggregate.ContainsKey(clave))
                            aggregate[clave] = (cantidad, item.Unit);
                        else
                            aggregate[clave] = (
                                Math.Max(aggregate[clave].Total, cantidad),
                                aggregate[clave].Unidad
                            );
                    }
                }
            }

            var items = new List<PlannedItem>();
            foreach (var kv in aggregate)
            {
                var nombre = kv.Key;
                var required = kv.Value.Total;
                var unit = kv.Value.Unidad;

                var match = supplies.FirstOrDefault(s =>
                    s.NombreNormalizado.Trim().ToLower() == nombre ||
                    s.Nombre.Trim().ToLower() == nombre);

                items.Add(new PlannedItem
                {
                    Description = match?.Nombre ?? nombre,
                    Unit = unit,
                    RequiredQuantity = required,
                    StockAvailable = match?.StockTotal ?? 0,
                    ExistsInSystem = match != null,
                    IdInsumo = match?.IdInsumo
                });
            }

            var plan = new SemesterPurchasePlan
            {
                Cycle = ciclo,
                Faculty = facultad,
                GeneratedAt = DateTime.UtcNow,
                Items = items
            };

            await _planRepo.SaveAsync(plan);
            return plan;
        }

        public async Task<List<SemesterPurchasePlan>> GetAllPlansAsync() => await _planRepo.GetAllAsync();

        public async Task<SemesterPurchasePlan?> GetPlanByIdAsync(string id) => await _planRepo.GetByIdAsync(id);
    }
}
