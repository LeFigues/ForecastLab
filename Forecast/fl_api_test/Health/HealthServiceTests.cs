using fl_api.Configurations;
using fl_api.Interfaces;
using fl_api.Services;
using Microsoft.Extensions.Options;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace fl_api_test.Health
{
    public class HealthServiceTests
    {
        [Fact]
        public async Task CheckHealthAsync_ReturnsCorrectStatus()
        {
            // Arrange
            var mockMongo = new Mock<IMongoDbService>();
            var mockOpenAI = new Mock<IOpenAIService>();
            var mockStudents = new Mock<IStudentsApiService>();
            var mockLabs = new Mock<ILabsApiService>();

            mockMongo.Setup(x => x.CanConnectAsync()).ReturnsAsync(true);
            mockOpenAI.Setup(x => x.CanConnectAsync()).ReturnsAsync(true);
            mockStudents.Setup(x => x.CanConnectAsync()).ReturnsAsync(false); // Simulamos fallo
            mockLabs.Setup(x => x.CanConnectAsync()).ReturnsAsync(true);

            var options = Options.Create(new PdfStorageSettings
            {
                BasePath = Path.Combine(Path.GetTempPath(), "test_health")
            });

            var service = new HealthService(
                mockMongo.Object,
                mockOpenAI.Object,
                mockStudents.Object,
                mockLabs.Object,
                options
            );

            // Act
            var result = await service.CheckHealthAsync();

            // Assert
            Assert.True(result.Mongo);
            Assert.True(result.OpenAI);
            Assert.False(result.StudentsApi);
            Assert.True(result.LabsApi);
            Assert.True(result.PdfStorage);
        }
    }
}
