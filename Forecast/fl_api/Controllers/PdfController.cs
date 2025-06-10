using fl_api.Configurations;
using fl_api.Dtos.Guides;
using fl_api.Interfaces.Guides;
using fl_api.Models.Guides;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using fl_api.Interfaces;

namespace fl_api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PdfController : ControllerBase
    {
        private readonly IGuideFileRepository _fileRepo;
        private readonly PdfStorageSettings _storage;
        private readonly IGuideAnalysisService _analysisService;
        private readonly IGuideAnalysisRepository _analysisRepo;
        private readonly IPdfExtractorService _pdfExtractor;
        private readonly IGuideAnalysisRepository _analysisRepository;
        private readonly IGuideGroupAnalysisService _guideGroupService;

        public PdfController(
            IGuideFileRepository fileRepo,
            IOptions<PdfStorageSettings> storage,
            IGuideAnalysisService analysisService,
            IGuideAnalysisRepository analysisRepo,
            IPdfExtractorService pdfExtractor,
            IGuideAnalysisRepository analysisRepository,
            IGuideGroupAnalysisService guideGroupService)
        {
            _fileRepo = fileRepo;
            _storage = storage.Value;
            _analysisService = analysisService;
            _analysisRepo = analysisRepo;
            _pdfExtractor = pdfExtractor;
            _analysisRepository = analysisRepository;
            _guideGroupService = guideGroupService;
        }

        [HttpPost("upload")]
        public async Task<IActionResult> Upload([FromForm] IFormFile file, [FromForm] string type)
        {
            if (file == null || file.Length == 0 || !file.FileName.EndsWith(".pdf"))
                return BadRequest("Invalid file");

            var folder = type == "guia" ? "guias" : "practicas";
            var pathFolder = Path.Combine(_storage.BasePath, folder);
            Directory.CreateDirectory(pathFolder);

            var fileName = $"{Guid.NewGuid()}_{file.FileName}";
            var fullPath = Path.Combine(pathFolder, fileName);

            using (var stream = new FileStream(fullPath, FileMode.Create))
                await file.CopyToAsync(stream);

            // Guardar metadatos del archivo
            var record = new GuideFileRecord
            {
                FileName = file.FileName,
                FilePath = fullPath,
                Tipo = type,
                UploadedAt = DateTime.UtcNow
            };

            await _fileRepo.SaveAsync(record);

            return Ok(new { id = record.Id.ToString(), file = record.FileName, type });
        }

        [HttpPost("analyze/{id}")]
        public async Task<IActionResult> Analyze(string id, [FromBody] GuideAnalysisRequest request)
        {
            // Buscar el archivo correspondiente
            var guideFile = await _fileRepo.GetByIdAsync(id);
            if (guideFile == null || !System.IO.File.Exists(guideFile.FilePath))
                return NotFound("PDF not found");

            // Extraer texto desde el archivo PDF usando PdfPig
            using var stream = System.IO.File.OpenRead(guideFile.FilePath);
            var formFile = new FormFile(stream, 0, stream.Length, null!, guideFile.FileName);
            var plainText = await _pdfExtractor.ExtractTextAsync(formFile);

            // Enviar texto a OpenAI u otro análisis
            var result = await _analysisService.AnalyzeRawTextAsync(id, plainText, request.Model, request.Type);

            return Ok(new
            {
                result.Id,
                result.GuideFileId,
                result.RegistryCode,
                result.Version,
                result.Subject,
                result.ModelUsed,
                result.CreatedAt,
                result.RawText,
                RawJson = MongoDB.Bson.BsonTypeMapper.MapToDotNetValue(result.RawJson)
            });
        }

        [HttpGet("files")]
        public async Task<ActionResult<List<GuideFileRecord>>> GetAllFiles()
        {
            var files = await _fileRepo.GetAllAsync();
            return Ok(files);
        }

        [HttpGet("analysis/{guideFileId}")]
        public async Task<IActionResult> GetAnalysis(string guideFileId)
        {
            var result = await _analysisRepo.GetByGuideFileIdAsync(guideFileId);
            if (result == null)
                return NotFound();

            return Ok(new
            {
                result.Id,
                result.GuideFileId,
                result.RegistryCode,
                result.Version,
                result.Subject,
                result.ModelUsed,
                result.CreatedAt,
                result.RawText,
                RawJson = MongoDB.Bson.BsonTypeMapper.MapToDotNetValue(result.RawJson)
            });
        }

        [HttpPost("auto")]
        public async Task<IActionResult> UploadAndAnalyze(
    [FromForm] IFormFile file,
    [FromForm] string type,
    [FromForm] string model)
        {
            if (file == null || file.Length == 0 || !file.FileName.EndsWith(".pdf"))
                return BadRequest("Invalid file");

            var folder = type == "guia" ? "guias" : "practicas";
            var pathFolder = Path.Combine(_storage.BasePath, folder);
            Directory.CreateDirectory(pathFolder);

            var fileName = $"{Guid.NewGuid()}_{file.FileName}";
            var fullPath = Path.Combine(pathFolder, fileName);

            using (var stream = new FileStream(fullPath, FileMode.Create))
                await file.CopyToAsync(stream);

            var record = new GuideFileRecord
            {
                FileName = file.FileName,
                FilePath = fullPath,
                Tipo = type,
                UploadedAt = DateTime.UtcNow
            };

            await _fileRepo.SaveAsync(record);

            // Extraer texto y analizar en una sola llamada
            using var fileStream = new FileStream(fullPath, FileMode.Open, FileAccess.Read);
            var formFile = new FormFile(fileStream, 0, fileStream.Length, null!, file.FileName);
            var text = await _pdfExtractor.ExtractTextAsync(formFile);

            var result = await _analysisService.AnalyzeRawTextAsync(record.Id.ToString(), text, model, type);

            return Ok(new
            {
                result.Id,
                result.GuideFileId,
                result.RegistryCode,
                result.Version,
                result.Subject,
                result.ModelUsed,
                result.CreatedAt,
                result.RawText,
                RawJson = MongoDB.Bson.BsonTypeMapper.MapToDotNetValue(result.RawJson)

            });
        }

        [HttpPost("auto/guide")]
        public async Task<IActionResult> UploadAndAnalyzeGuide([FromForm] GuideGroupAnalysisRequest request)
        {
            var file = request.File;
            var model = request.Model;

            if (file == null || file.Length == 0 || !file.FileName.EndsWith(".pdf"))
                return BadRequest("Invalid file");

            var path = Path.Combine(_storage.BasePath, "guias");
            Directory.CreateDirectory(path);
            var fileName = Guid.NewGuid() + "_" + file.FileName;
            var fullPath = Path.Combine(path, fileName);

            using (var stream = new FileStream(fullPath, FileMode.Create))
                await file.CopyToAsync(stream);

            var record = new GuideFileRecord
            {
                FileName = file.FileName,
                FilePath = fullPath,
                Tipo = "guia",
                UploadedAt = DateTime.UtcNow
            };
            await _fileRepo.SaveAsync(record);

            using var fileStream = new FileStream(fullPath, FileMode.Open);
            var formFile = new FormFile(fileStream, 0, fileStream.Length, null!, file.FileName);
            var text = await _pdfExtractor.ExtractTextAsync(formFile);

            var result = await _guideGroupService.AnalyzeGuideAsync(record.Id.ToString(), text, model);

            return Ok(new
            {
                result.Id,
                result.GuideFileId,
                result.RegistryCode,
                result.Version,
                result.Subject,
                result.ModelUsed,
                result.CreatedAt,
                result.RawText,
                RawJson = MongoDB.Bson.BsonTypeMapper.MapToDotNetValue(result.RawJson)
            });
        }


    }
}
