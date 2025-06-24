using Xunit;
using Moq;
using fl_api.Interfaces.University;
using fl_api.Models.Guides;
using fl_api.Models.University;
using fl_api.Services.Guides;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace fl_api_test.Guides
{
    public class LabGuideVerificationServiceTests
    {
        [Fact]
        public async Task VerifyAgainstStockAsync_HandlesExactMatchesSuggestionsAndUnmatched()
        {
            // Arrange
            var supplies = new List<NormalizedSupply>
            {
                new() { IdInsumo = 1, Nombre = "Multímetro", NombreNormalizado = "multimetro", StockTotal = 10, UnidadMedida = "unidad" },
                new() { IdInsumo = 2, Nombre = "Guantes Latex", NombreNormalizado = "guantes latex", StockTotal = 100, UnidadMedida = "pares" }
            };

            var guide = new LabGuide
            {
                Practices = new List<LabPractice>
                {
                    new()
                    {
                        GroupCount = 2,
                        Materials = new LabMaterials
                        {
                            Equipment = new List<LabItem>
                            {
                                new() { Description = "Multimetro", QuantityPerGroup = 1, Unit = "unidad" }
                            },
                            Supplies = new List<LabItem>
                            {
                                new() { Description = "Guantes", QuantityPerGroup = 2, Unit = "pares" },
                                new() { Description = "Reactivo Desconocido", QuantityPerGroup = 5, Unit = "ml" }
                            }
                        }
                    }
                }
            };

            var mockRepo = new Mock<INormalizedSupplyRepository>();
            mockRepo.Setup(r => r.GetAllAsync()).ReturnsAsync(supplies);

            var service = new LabGuideVerificationService(mockRepo.Object);

            // Act
            var result = await service.VerifyAgainstStockAsync(guide);

            // Assert
            Assert.Equal(2, result.Verificados.Count);
            Assert.Single(result.NoEncontrados);

            var multimetro = result.Verificados.FirstOrDefault(v => v.Nombre == "Multímetro");
            Assert.NotNull(multimetro);
            Assert.False(multimetro.EsSugerencia);
            Assert.Equal(2, multimetro.CantidadTotal); // 1 * 2 grupos

            var guantes = result.Verificados.FirstOrDefault(v => v.Nombre == "Guantes Latex");
            Assert.NotNull(guantes);
            Assert.True(guantes.EsSugerencia); // porque el nombre original era solo "Guantes"
            Assert.Equal(4, guantes.CantidadTotal); // 2 * 2 grupos

            var reactivo = result.NoEncontrados.FirstOrDefault(n => n.Nombre == "reactivo desconocido");
            Assert.NotNull(reactivo);
            Assert.Equal(10, reactivo.CantidadTotal); // 5 * 2 grupos
        }
    }
}
