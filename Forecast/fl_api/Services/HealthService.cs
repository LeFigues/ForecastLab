using fl_api.Configurations;
using fl_api.Dtos;
using fl_api.Interfaces;
using Microsoft.Extensions.Options;

namespace fl_api.Services
{
    public class HealthService : IHealthService
    {
        private readonly IMongoDbService _mongo;
        private readonly IOpenAIService _openAi;
        private readonly IStudentsApiService _students;
        private readonly ILabsApiService _labs;
        private readonly PdfStorageSettings _pdfStorage;

        public HealthService(
            IMongoDbService mongo,
            IOpenAIService openAi,
            IStudentsApiService students,
            ILabsApiService labs,
            IOptions<PdfStorageSettings> pdfStorage
        )
        {
            _mongo = mongo;
            _openAi = openAi;
            _students = students;
            _labs = labs;
            _pdfStorage = pdfStorage.Value;
        }

        public async Task<HealthStatusDto> CheckHealthAsync()
        {
            var mongoTask = _mongo.CanConnectAsync();
            var openAiTask = _openAi.CanConnectAsync();
            var studentsTask = _students.CanConnectAsync();
            var labsTask = _labs.CanConnectAsync();

            var pdfStorageOk = CheckPdfStorageAccess();

            await Task.WhenAll(mongoTask, openAiTask, studentsTask, labsTask);

            return new HealthStatusDto
            {
                Mongo = mongoTask.Result,
                OpenAI = openAiTask.Result,
                StudentsApi = studentsTask.Result,
                LabsApi = labsTask.Result,
                PdfStorage = pdfStorageOk,
            };
        }

        private bool CheckPdfStorageAccess()
        {
            try
            {
                Directory.CreateDirectory(_pdfStorage.BasePath);
                var testFile = Path.Combine(_pdfStorage.BasePath, ".test_access");
                File.WriteAllText(testFile, "ping");
                File.Delete(testFile);
                return true;
            }
            catch
            {
                return false;
            }
        }

        
    }
}
