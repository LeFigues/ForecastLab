using fl_api.Dtos.Forecast;
using fl_api.Interfaces.University;
using fl_api.Repositories.Forecast;
using fl_api.Services.Forecast;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using University.Interfaces;

namespace fl_api_test.Forecast
{
    public class ForecastServiceTests
    {
        private ForecastService CreateService()
        {
            // Dependencias simuladas (no usadas por ForecastAsync)
            var mockApi = new Mock<IUniversityApiClient>();
            var mockRiesgoRepo = new Mock<IForecastRiesgoRepository>();
            var mockHistoricoRepo = new Mock<IForecastHistoricoRepository>();
            var mockPracticaRepo = new Mock<IForecastPracticaRepository>();
            var mockSemestreRepo = new Mock<IForecastSemestreRepository>();

            return new ForecastService(
                mockApi.Object,
                mockRiesgoRepo.Object,
                mockHistoricoRepo.Object,
                mockPracticaRepo.Object,
                mockSemestreRepo.Object
            );
        }

        [Fact]
        public async Task ForecastAsync_WithSinglePoint_ReturnsRepeatedValue()
        {
            // Arrange
            var history = new List<DailyDemandDto>
            {
                new() { Date = new DateTime(2025, 1, 1), Quantity = 10 }
            };

            var service = CreateService();

            // Act
            var result = await service.ForecastAsync(history, "monthly");

            // Assert
            Assert.Equal(6, result.Count);
            Assert.All(result, p => Assert.Equal(10, p.ForecastedQuantity));
        }

        [Fact]
        public async Task ForecastAsync_WithMultiplePoints_ReturnsRegressionForecast()
        {
            // Arrange
            var history = new List<DailyDemandDto>
            {
                new() { Date = new DateTime(2025, 1, 1), Quantity = 10 },
                new() { Date = new DateTime(2025, 2, 1), Quantity = 20 },
                new() { Date = new DateTime(2025, 3, 1), Quantity = 30 }
            };

            var service = CreateService();

            // Act
            var result = await service.ForecastAsync(history, "monthly");

            // Assert
            Assert.Equal(6, result.Count);
            Assert.True(result[0].ForecastedQuantity > 30); // crecimiento lineal esperado
        }

        [Fact]
        public async Task ForecastAsync_WithEmptyHistory_ReturnsEmptyList()
        {
            // Arrange
            var history = new List<DailyDemandDto>();
            var service = CreateService();

            // Act
            var result = await service.ForecastAsync(history, "monthly");

            // Assert
            Assert.Empty(result);
        }

        [Fact]
        public async Task ForecastAsync_WithInvalidHorizon_ThrowsException()
        {
            // Arrange
            var history = new List<DailyDemandDto>
            {
                new() { Date = new DateTime(2025, 1, 1), Quantity = 10 }
            };
            var service = CreateService();

            // Act & Assert
            await Assert.ThrowsAsync<ArgumentException>(() => service.ForecastAsync(history, "diario"));
        }
    }
}
