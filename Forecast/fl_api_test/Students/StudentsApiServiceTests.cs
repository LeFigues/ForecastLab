using fl_api.Services;
using Moq.Protected;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Json;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using fl_api.Dtos.StudentsApi;

namespace fl_api_test.Students
{
    public class StudentsApiServiceTests
    {
        [Fact]
        public async Task GetFacultiesAsync_ReturnsFacultyList()
        {
            // Arrange
            var expectedFaculties = new List<FacultyDto>
            {
                new() { Id = "1", Name = "Ingeniería" },
                new() { Id = "2", Name = "Ciencias de la Salud" }
            };

            var mockHandler = new Mock<HttpMessageHandler>();
            mockHandler
                .Protected()
                .Setup<Task<HttpResponseMessage>>("SendAsync",
                    ItExpr.Is<HttpRequestMessage>(r => r.RequestUri!.ToString().Contains("api/faculty")),
                    ItExpr.IsAny<CancellationToken>())
                .ReturnsAsync(new HttpResponseMessage
                {
                    StatusCode = HttpStatusCode.OK,
                    Content = JsonContent.Create(expectedFaculties)
                });

            var client = new HttpClient(mockHandler.Object)
            {
                BaseAddress = new Uri("https://api.students.fake")
            };

            var service = new StudentsApiService(client);

            // Act
            var result = await service.GetFacultiesAsync();

            // Assert
            Assert.NotNull(result);
            Assert.Equal(2, result.Count);
            Assert.Equal("Ingeniería", result[0].Name);
            Assert.Equal("2", result[1].Id);
        }
    }
}
