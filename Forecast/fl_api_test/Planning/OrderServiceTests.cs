using fl_api.Dtos.Planification;
using fl_api.Interfaces.Planification;
using fl_api.Models.Planification;
using fl_api.Services.Planification;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace fl_api_test.Planning
{
    public class OrderServiceTests
    {
        [Fact]
        public async Task CreateAsync_CreatesOrderWithCorrectTotalAndFields()
        {
            // Arrange
            var dto = new PurchaseOrderCreateDto
            {
                Proveedor = "Proveedor A",
                Notas = "Entrega urgente",
                Items = new List<PurchaseOrderItemDto>
                {
                    new()
                    {
                        Insumo = "Multímetro",
                        Cantidad = 5,
                        PrecioUnitario = 20.5m,
                        FechaEntregaDeseada = new DateTime(2025, 7, 15)
                    },
                    new()
                    {
                        Insumo = "Guantes",
                        Cantidad = 10,
                        PrecioUnitario = 3.0m,
                        FechaEntregaDeseada = new DateTime(2025, 7, 20)
                    }
                }
            };

            var mockRepo = new Mock<IPurchaseOrderRepository>();
            mockRepo.Setup(r => r.SaveAsync(It.IsAny<PurchaseOrder>())).Returns(Task.CompletedTask);

            var service = new OrderService(mockRepo.Object);

            // Act
            var result = await service.CreateAsync(dto);

            // Assert
            Assert.NotNull(result);
            Assert.Equal("Proveedor A", result.Proveedor);
            Assert.Equal("Pendiente", result.Estado);
            Assert.Equal("Entrega urgente", result.Notas);
            Assert.StartsWith("ORD-", result.NumeroOrden);
            Assert.Equal(2, result.Items.Count);

            decimal expectedTotal = 5 * 20.5m + 10 * 3.0m;
            Assert.Equal(expectedTotal, result.TotalMonto);

            var multimetro = result.Items.Find(i => i.Insumo == "Multímetro");
            Assert.NotNull(multimetro);
            Assert.Equal(5, multimetro.Cantidad);
            Assert.Equal(20.5m, multimetro.PrecioUnitario);

            mockRepo.Verify(r => r.SaveAsync(It.IsAny<PurchaseOrder>()), Times.Once);
        }
    }
}
