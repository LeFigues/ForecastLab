using fl_api.Interfaces.Guides;
using fl_api.Models.Guides;

namespace fl_api.Services.Guides
{
    public class SemesterGuideAnalysisService : ISemesterGuideAnalysisService
    {
        private readonly IGuideFileRepository _fileRepo;
        private readonly ILabGuideRepository _labGuideRepo;
        private readonly ILabGuideExtractionService _extractionService;

        public SemesterGuideAnalysisService(
            IGuideFileRepository fileRepo,
            ILabGuideRepository labGuideRepo,
            ILabGuideExtractionService extractionService)
        {
            _fileRepo = fileRepo;
            _labGuideRepo = labGuideRepo;
            _extractionService = extractionService;
        }

        public async Task<List<LabGuide>> AnalyzeAllUnprocessedAsync(string ciclo, string facultad)
        {
            var files = await _fileRepo.GetByCycleAndFacultyAsync(ciclo, facultad);
            var results = new List<LabGuide>();

            foreach (var file in files)
            {
                var alreadyAnalyzed = await _labGuideRepo.GetByGuideFileIdAsync(file.Id.ToString());
                if (alreadyAnalyzed != null)
                    continue;

                try
                {
                    var result = await _extractionService.AnalyzeGuideFromPdfAsync(file.Id.ToString());
                    results.Add(result);
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"⚠️ Error analizando {file.FileName}: {ex.Message}");
                    // puedes loguear o guardar un estado fallido si lo deseas
                }
            }

            return results;
        }
    }
}
