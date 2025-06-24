using fl_api.Dtos.Reports;
using fl_api.Dtos.University;
using fl_api.Interfaces.Reports;
using fl_api.Interfaces.University;
using fl_api.Interfaces;
using fl_api.Models.Reports;
using fl_api.Services.Reports;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using University.Interfaces;
using University.Models;

namespace fl_api_test.Reports
{
    public class ReportServiceTests
    {
        [Fact]
        public async Task GetUnidadesAComprarAsync_ReturnsExpectedForecastAndPrices()
        {
            // Arrange
            var filter = new ReportFilterDto
            {
                From = new DateTime(2025, 7, 1),
                To = new DateTime(2025, 12, 31),
                Category = null,
                LabId = null
            };

            var forecast = new List<MovimientoResumenDto>
            {
                new() { Insumo = "Multímetro", CantidadTotal = 10 },
                new() { Insumo = "Protoboard", CantidadTotal = 20 }
            };

            var insumos = new List<Insumo>
            {
                new() { Nombre = "Multímetro", StockActual = 3, Ubicacion = "Lab 1" },
                new() { Nombre = "Protoboard", StockActual = 0, Ubicacion = "Lab 2" }
            };

            var preciosExistentes = new List<PrecioEstimadoRecord>
            {
                new() { Nombre = "Multímetro", Precio = 120.0m }
            };

            var gptJson = @"[
                { ""nombre"": ""Protoboard"", ""precio"": 15.5 }
            ]";

            var mockUniversity = new Mock<IUniversityApiClient>();
            var mockForecast = new Mock<IUniversityForecastService>();
            var mockOpenAi = new Mock<IOpenAIService>();
            var mockPrecioRepo = new Mock<IPrecioEstimadoRepository>();

            mockForecast.Setup(x => x.GetForecastPorRangoAsync(filter.From, filter.To)).ReturnsAsync(forecast);
            mockUniversity.Setup(x => x.GetInsumosAsync()).ReturnsAsync(insumos);
            mockPrecioRepo.Setup(x => x.GetAllAsync()).ReturnsAsync(preciosExistentes);
            mockOpenAi.Setup(x => x.AnalyzeTextAsync(It.IsAny<string>(), "gpt-4o")).ReturnsAsync(gptJson);
            mockPrecioRepo.Setup(x => x.SaveManyAsync(It.IsAny<IEnumerable<PrecioEstimadoRecord>>())).Returns(Task.CompletedTask);

            var service = new ReportService(
                mockUniversity.Object,
                mockForecast.Object,
                mockOpenAi.Object,
                mockPrecioRepo.Object
            );

            // Act
            var result = await service.GetUnidadesAComprarAsync(filter);

            // Assert
            Assert.Equal(2, result.Count);

            var multimetro = result.Find(r => r.SupplyName == "Multímetro");
            Assert.NotNull(multimetro);
            Assert.Equal(10, multimetro.ForecastedDemand);
            Assert.Equal(3, multimetro.CurrentStock);
            Assert.Equal(120.0m, multimetro.UnitCost);

            var protoboard = result.Find(r => r.SupplyName == "Protoboard");
            Assert.NotNull(protoboard);
            Assert.Equal(20, protoboard.ForecastedDemand);
            Assert.Equal(0, protoboard.CurrentStock);
            Assert.Equal(15.5m, protoboard.UnitCost);

            mockPrecioRepo.Verify(p => p.SaveManyAsync(It.IsAny<IEnumerable<PrecioEstimadoRecord>>()), Times.Once);
        }
    }
}
