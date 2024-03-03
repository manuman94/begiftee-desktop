using System.Net;
using Moq;
using NUnit.Framework;
using BeGiftee.Source.Services.Network;
using NUnit.Framework.Legacy;

namespace BeGiftee.Test.Services.HttpServiceTests
{
    [TestFixture]
    public class HttpServiceTests
    {
        private HttpService _httpService;
        private Mock<HttpClient> _mockHttpClient;

        [SetUp]
        public void Setup()
        {
            _httpService = new HttpService();
            _mockHttpClient = new Mock<HttpClient>();
        }

        [Test]
        public async Task GetAsync_Success_ReturnsDeserializedObject()
        {
            // Arrange
            var expectedResponse = new YourDto {};
            var httpResponseMessage = new HttpResponseMessage(HttpStatusCode.OK)
            {
                Content = new StringContent("{\"property\": \"value\"}")
            };
            _mockHttpClient.Setup(client => client.GetAsync(It.IsAny<string>(), It.IsAny<CancellationToken>()))
                           .ReturnsAsync(httpResponseMessage);

            // Act
            var result = await _httpService.GetAsync<YourDto>("http://example.com/api/resource");

            // Assert
            ClassicAssert.NotNull(result);
        }

        [Test]
        public async Task GetAsync_Failure_ThrowsHttpRequestException()
        {
            // Arrange
            var httpResponseMessage = new HttpResponseMessage(HttpStatusCode.NotFound);
            _mockHttpClient.Setup(client => client.GetAsync(It.IsAny<string>(), It.IsAny<CancellationToken>()))
                           .ReturnsAsync(httpResponseMessage);

            // Act & Assert
            Assert.ThrowsAsync<HttpRequestException>(async () =>
            {
                await _httpService.GetAsync<YourDto>("http://example.com/api/resource");
            });
        }

    }

    // Example DTO class for testing
    public class YourDto {}
}
