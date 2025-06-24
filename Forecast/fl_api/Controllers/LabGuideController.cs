using fl_api.Interfaces.Guides;
using fl_api.Repositories.Guides;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace fl_api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LabGuideController : ControllerBase
    {
        private readonly ILabGuideExtractionService _extractionService;
        private readonly ILabGuideRepository _labGuideRepository;
        private readonly ILabGuideVerificationService _verificationService;
        private readonly IGuideUploadService _uploadService;
        private readonly ISemesterGuideAnalysisService _semesterAnalysisService;

        public LabGuideController(
            ILabGuideExtractionService extractionService,
            ILabGuideRepository labGuideRepository,
            ILabGuideVerificationService verificationService,
            IGuideUploadService uploadService,
            ISemesterGuideAnalysisService semesterAnalysisService)
        {
            _extractionService = extractionService;
            _labGuideRepository = labGuideRepository;
            _verificationService = verificationService;
            _uploadService = uploadService;
            _semesterAnalysisService = semesterAnalysisService;
        }



        // GET: /api/labguide
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var guides = await _extractionService.GetAllAsync();
            return Ok(guides);
        }

        // POST: /api/labguide/analyze/{fileId}
        [HttpPost("analyze/{fileId}")]
        public async Task<IActionResult> Analyze(string fileId, [FromQuery] string model = "gpt-4o")
        {
            try
            {
                var result = await _extractionService.AnalyzeGuideFromPdfAsync(fileId, model);
                return Ok(result);
            }
            catch (FileNotFoundException ex)
            {
                return NotFound(new { error = ex.Message });
            }
            catch (Exception ex)
            {
                return BadRequest(new { error = ex.Message });
            }
        }

        // POST: /api/labguide/verify/{id}
        [HttpPost("verify/{id}")]
        public async Task<IActionResult> VerifyGuideMaterials(string id)
        {
            var guide = await _labGuideRepository.GetByIdAsync(id);
            if (guide == null)
                return NotFound("No se encontró la guía.");

            var result = await _verificationService.VerifyAgainstStockAsync(guide);
            return Ok(result);
        }

        [HttpPost("upload-batch")]
        public async Task<IActionResult> UploadBatch(
            [FromForm] string ciclo,
            [FromForm] string facultad,
            [FromForm] List<IFormFile> files,
            [FromForm] List<string> carreras,
            [FromForm] List<string> materias)
        {
            if (files.Count != carreras.Count || files.Count != materias.Count)
                return BadRequest("Cada archivo debe tener su carrera y materia correspondiente.");

            var results = new List<object>();

            for (int i = 0; i < files.Count; i++)
            {
                var file = files[i];
                var carrera = carreras[i];
                var materia = materias[i];

                try
                {
                    var saved = await _uploadService.SaveGuideAsync(file, ciclo, facultad, carrera, materia);
                    results.Add(new { file = file.FileName, status = "ok", id = saved.Id.ToString() });
                }
                catch (Exception ex)
                {
                    results.Add(new { file = file.FileName, status = "error", error = ex.Message });
                }
            }

            return Ok(results);
        }

        [HttpPost("analyze-all")]
        public async Task<IActionResult> AnalyzeAllInCycle([FromQuery] string ciclo, [FromQuery] string facultad)
        {
            var results = await _semesterAnalysisService.AnalyzeAllUnprocessedAsync(ciclo, facultad);

            // Obtener los archivos guía para cruzar datos como carrera y materia
            var allFiles = await _uploadService.GetAllAsync();
            var analizadas = results.Select(r =>
            {
                var file = allFiles.FirstOrDefault(f => f.Id.ToString() == r.GuideFileId);
                return new
                {
                    id = r.Id,
                    guideFileId = r.GuideFileId,
                    nombreArchivo = file?.FileName ?? "(desconocido)",
                    materia = file?.Subject ?? "(sin materia)",
                    carrera = file?.Career ?? "(sin carrera)"
                };
            }).ToList();

            return Ok(new
            {
                procesadas = analizadas.Count,
                analizadas
            });
        }
        [HttpGet("analyzed")]
        public async Task<IActionResult> GetAllAnalyzed()
        {
            var guides = await _labGuideRepository.GetAllAsync();
            return Ok(guides);
        }

        // GET: /api/labguide/analyzed/{id}
        [HttpGet("analyzed/{id}")]
        public async Task<IActionResult> GetAnalyzedById(string id)
        {
            var guide = await _labGuideRepository.GetByIdAsync(id);
            if (guide == null)
                return NotFound(new { message = "No se encontró la guía analizada con ese ID." });

            return Ok(guide);
        }

    }
}
