using fl_api.Interfaces.University;
using fl_api.Interfaces;
using fl_api.Services.University;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using fl_api.Models.University;

namespace fl_api_test.Supplies
{
    public class SupplyUpdateServiceTests
    {
        [Fact]
        public async Task UpdateByIdsAsync_UpdatesMatchingInsumos_WithValuesFromAI()
        {
            // Arrange
            var insumo = new NormalizedSupply
            {
                IdInsumo = 1,
                Nombre = "Multímetro",
                Descripcion = "Dispositivo para medir voltaje",
                PrecioEstimado = 0,
                VidaUtilMeses = 0,
                PrecioGeneradoPorIA = false,
                VidaUtilGeneradaPorIA = false
            };

            var jsonMock = @"{
              ""resultados"": [
                { ""nombre"": ""Multímetro"", ""precio"": 110.50, ""vidaUtil"": 24 }
              ]
            }";

            var mockRepo = new Mock<INormalizedSupplyRepository>();
            mockRepo.Setup(r => r.GetAllAsync()).ReturnsAsync(new List<NormalizedSupply> { insumo });
            mockRepo.Setup(r => r.UpsertAsync(It.IsAny<NormalizedSupply>())).Returns(Task.CompletedTask);

            var mockAI = new Mock<IOpenAIService>();
            mockAI.Setup(ai => ai.AnalyzeTextAsync(It.IsAny<string>(), "gpt-4")).ReturnsAsync(jsonMock);

            var service = new SupplyUpdateService(mockRepo.Object, mockAI.Object);

            // Act
            var result = await service.UpdateByIdsAsync(new List<int> { 1 });

            // Assert
            Assert.Single(result);
            var updated = result[0];
            Assert.Equal(110.50m, updated.PrecioEstimado);
            Assert.Equal(24, updated.VidaUtilMeses);
            Assert.True(updated.PrecioGeneradoPorIA);
            Assert.True(updated.VidaUtilGeneradaPorIA);
            mockRepo.Verify(r => r.UpsertAsync(It.Is<NormalizedSupply>(x => x.IdInsumo == 1)), Times.Once);
        }
    }
}
