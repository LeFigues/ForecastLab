using fl_api.Interfaces.Guides;
using fl_api.Interfaces.Students;
using fl_api.Interfaces.University;
using fl_api.Interfaces;
using fl_api.Models.Students;

namespace fl_api.Services.Students
{
    public class StudentDemandForecastService : IStudentDemandForecastService
    {
        private readonly IStudentsApiService _studentsApi;
        private readonly ILabGuideRepository _labGuideRepo;
        private readonly INormalizedSupplyRepository _supplyRepo;
        private readonly IStudentDemandForecastRepository _reportRepo;

        public StudentDemandForecastService(
            IStudentsApiService studentsApi,
            ILabGuideRepository labGuideRepo,
            INormalizedSupplyRepository supplyRepo,
            IStudentDemandForecastRepository reportRepo)
        {
            _studentsApi = studentsApi;
            _labGuideRepo = labGuideRepo;
            _supplyRepo = supplyRepo;
            _reportRepo = reportRepo;
        }

        public async Task<StudentDemandReport> GenerateAsync(string ciclo, string facultad)
        {
            var groups = await _studentsApi.GetGroupsFullAsync();
            var guides = await _labGuideRepo.GetByCycleAndFacultyAsync(ciclo, facultad);
            var supplies = await _supplyRepo.GetAllAsync();

            var maxStudents = groups.Max(g => g.StudentCount);
            var minStudentsPerGroup = guides.SelectMany(g => g.Practices).Min(p => p.StudentsPerGroup);
            var calculatedGroups = (int)Math.Ceiling(maxStudents / (double)minStudentsPerGroup);

            var aggregated = new Dictionary<string, (int maxQtyPerGroup, string unidad)>(StringComparer.OrdinalIgnoreCase);

            foreach (var guide in guides)
            {
                foreach (var practice in guide.Practices)
                {
                    foreach (var item in practice.Materials.Equipment.Concat(practice.Materials.Supplies))
                    {
                        var clave = item.Description.Trim().ToLower();
                        if (!aggregated.ContainsKey(clave))
                            aggregated[clave] = (item.QuantityPerGroup, item.Unit);
                        else
                            aggregated[clave] = (
                                Math.Max(aggregated[clave].maxQtyPerGroup, item.QuantityPerGroup),
                                item.Unit
                            );
                    }
                }
            }

            var report = new StudentDemandReport
            {
                Cycle = ciclo,
                Faculty = facultad,
                GeneratedAt = DateTime.UtcNow,
                Items = aggregated.Select(kv =>
                {
                    var totalQty = kv.Value.maxQtyPerGroup * calculatedGroups;
                    var match = supplies.FirstOrDefault(s =>
                        s.NombreNormalizado.Trim().ToLower() == kv.Key ||
                        s.Nombre.Trim().ToLower() == kv.Key);

                    return new StudentDemandItem
                    {
                        Description = match?.Nombre ?? kv.Key,
                        Unit = kv.Value.unidad,
                        RequiredQuantity = totalQty,
                        TotalGroups = calculatedGroups,
                        TotalStudents = maxStudents,
                        StockAvailable = match?.StockTotal ?? 0,
                        ExistsInSystem = match != null,
                        IdInsumo = match?.IdInsumo
                    };
                }).ToList()
            };

            await _reportRepo.SaveAsync(report);
            return report;
        }

        public async Task<List<StudentDemandReport>> GetAllAsync() => await _reportRepo.GetAllAsync();

        public async Task<StudentDemandReport?> GetByIdAsync(string id) => await _reportRepo.GetByIdAsync(id);
    }
}
