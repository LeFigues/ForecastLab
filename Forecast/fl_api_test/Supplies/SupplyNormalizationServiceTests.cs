using fl_api.Interfaces.University;
using fl_api.Interfaces;
using fl_api.Services.University;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using University.Interfaces;
using fl_api.Models.University;
using University.Models;

namespace fl_api_test.Supplies
{
    public class SupplyNormalizationServiceTests
    {
        [Fact]
        public async Task NormalizeAndSyncAsync_WithExistingSupply_SkipsUpsert()
        {
            // Arrange
            var insumo = new Insumo
            {
                IdInsumo = 1,
                Nombre = "Frasco de Vidrio",
                Descripcion = "Contenedor de vidrio resistente",
                Tipo = "Equipo",
                UnidadMedida = "unidad",
                StockActual = 10,
                StockMinimo = 2,
                StockMaximo = 20
            };

            var existing = new NormalizedSupply
            {
                IdInsumo = 1,
                Nombre = insumo.Nombre,
                NombreNormalizado = "frasco de vidrio"
            };

            var mockApi = new Mock<IUniversityApiClient>();
            mockApi.Setup(x => x.GetInsumosAsync()).ReturnsAsync(new List<Insumo> { insumo });

            var mockRepo = new Mock<INormalizedSupplyRepository>();
            mockRepo.Setup(x => x.GetByIdInsumoAsync(insumo.IdInsumo)).ReturnsAsync(existing);

            var mockOpenAI = new Mock<IOpenAIService>();

            var service = new SupplyNormalizationService(mockApi.Object, mockRepo.Object, mockOpenAI.Object);

            // Act
            var result = await service.NormalizeAndSyncAsync(forceRefresh: false);

            // Assert
            Assert.Single(result);
            Assert.Equal("frasco de vidrio", result[0].NombreNormalizado);
            mockRepo.Verify(x => x.UpsertAsync(It.IsAny<NormalizedSupply>()), Times.Never);
        }

        [Fact]
        public async Task NormalizeAndSyncAsync_WhenNotFound_InsertsNormalized()
        {
            // Arrange
            var insumo = new Insumo
            {
                IdInsumo = 2,
                Nombre = "Tubo Ensayo",
                Descripcion = "Tubo de vidrio para pruebas",
                Tipo = "Insumo",
                UnidadMedida = "unidad",
                StockActual = 30,
                StockMinimo = 5,
                StockMaximo = 50
            };

            var mockApi = new Mock<IUniversityApiClient>();
            mockApi.Setup(x => x.GetInsumosAsync()).ReturnsAsync(new List<Insumo> { insumo });

            var mockRepo = new Mock<INormalizedSupplyRepository>();
            mockRepo.Setup(x => x.GetByIdInsumoAsync(insumo.IdInsumo)).ReturnsAsync((NormalizedSupply?)null);

            var mockOpenAI = new Mock<IOpenAIService>();

            var service = new SupplyNormalizationService(mockApi.Object, mockRepo.Object, mockOpenAI.Object);

            // Act
            var result = await service.NormalizeAndSyncAsync();

            // Assert
            Assert.Single(result);
            var normalized = result[0];
            Assert.Equal("tubo ensayo", normalized.NombreNormalizado);
            Assert.Equal(0m, normalized.PrecioEstimado);
            Assert.Equal(12, normalized.VidaUtilMeses);
            mockRepo.Verify(x => x.UpsertAsync(It.IsAny<NormalizedSupply>()), Times.Once);
        }
    }
}
