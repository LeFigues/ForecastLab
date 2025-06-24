using fl_api.Configurations;
using fl_api.Interfaces.Guides;
using fl_api.Models.Guides;
using Microsoft.Extensions.Options;

namespace fl_api.Services.Guides
{
    public class GuideUploadService : IGuideUploadService
    {
        private readonly IGuideFileRepository _repository;
        private readonly PdfStorageSettings _storage;

        public GuideUploadService(IGuideFileRepository repository, IOptions<PdfStorageSettings> storageOptions)
        {
            _repository = repository;
            _storage = storageOptions.Value;
        }
        public async Task<List<GuideFileRecord>> GetAllAsync()
        {
            return await _repository.GetAllAsync();
        }

        public async Task<GuideFileRecord> SaveGuideAsync(IFormFile file, string ciclo, string facultad, string carrera, string materia)
        {
            if (file == null || file.Length == 0 || !file.FileName.EndsWith(".pdf"))
                throw new ArgumentException("Archivo inválido.");

            var folder = Path.Combine(_storage.BasePath, "guias");
            Directory.CreateDirectory(folder);

            var fileName = $"{Guid.NewGuid()}_{file.FileName}";
            var fullPath = Path.Combine(folder, fileName);

            using var stream = new FileStream(fullPath, FileMode.Create);
            await file.CopyToAsync(stream);

            var record = new GuideFileRecord
            {
                FileName = file.FileName,
                FilePath = fullPath,
                Tipo = "guia",
                UploadedAt = DateTime.UtcNow,
                Faculty = facultad,
                Career = carrera,
                Subject = materia,
                Cycle = ciclo
            };

            await _repository.SaveAsync(record);
            return record;
        }
    }
}
