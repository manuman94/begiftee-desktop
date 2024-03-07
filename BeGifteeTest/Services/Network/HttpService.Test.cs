using BeGiftee.Source.Services.Network;
using BeGiftee.Source.Services.Network.Exceptions;
using Moq;
using Moq.Protected;
using Newtonsoft.Json;
using System.Net;

namespace BeGifteeTest.Services.Network
{
    [TestFixture]
    public class HttpServiceTests
    {
        [Test]
        public async Task SetJwtToken_SetsAuthorizationHeaderCorrectly()
        {
            Mock<HttpMessageHandler> httpMessageHandlerMock = HttpServiceFixtures.GetMockedHttpMessageHandler("", HttpMethod.Get, HttpStatusCode.OK, "");
            HttpService _httpService = HttpServiceFixtures.InjectMockToHttpService(httpMessageHandlerMock);

            // Arrange
            var jwtToken = "test-jwt-token";

            // Act
            _httpService.SetJwtToken(jwtToken);
            await _httpService.GetAsync<EmptyDto>("");

            // Assert
            httpMessageHandlerMock.Protected().Verify(
                "SendAsync",
                Times.Once(),
                ItExpr.Is<HttpRequestMessage>(req =>
                    req.Headers.Authorization.Scheme == "Bearer" &&
                    req.Headers.Authorization.Parameter == jwtToken),
                ItExpr.IsAny<CancellationToken>()
            );
        }

        [Test]
        public async Task GetAsync_ReturnsDeserializedObjectOnSuccess()
        {
            // Arrange
            const string expectedUri = "test/endpoint";
            var expectedData = new DummyDto { Id = 1, Name = "TestName" };
            var jsonResponse = JsonConvert.SerializeObject(expectedData);
            Mock<HttpMessageHandler> httpMessageHandlerMock = HttpServiceFixtures.GetMockedHttpMessageHandler(expectedUri, HttpMethod.Get, HttpStatusCode.OK, jsonResponse);
            HttpService httpService = HttpServiceFixtures.InjectMockToHttpService(httpMessageHandlerMock);

            // Act
            var result = await httpService.GetAsync<DummyDto>(expectedUri);

            // Assert
            Assert.IsNotNull(result);
            Assert.That(result.Name, Is.EqualTo(expectedData.Name));
            httpMessageHandlerMock.Protected().Verify(
                "SendAsync",
                Times.Once(),
                ItExpr.Is<HttpRequestMessage>(req =>
                    req.Method == HttpMethod.Get &&
                    req.RequestUri.ToString().Contains(expectedUri)),
                ItExpr.IsAny<CancellationToken>()
            );
        }

        [Test]
        public async Task PostAsync_SendsDataAndReturnsDeserializedObjectOnSuccess()
        {
            // Arrange
            var expectedUri = "test/postEndpoint";
            var payloadExpectedData = new DummyDto { Id = 1, Name = "Payload" };
            var returnExpectedData = new DummyDto { Id = 2, Name = "ReturnValue" };
            var jsonResponse = JsonConvert.SerializeObject(returnExpectedData);
            Mock<HttpMessageHandler> httpMessageHandlerMock = HttpServiceFixtures.GetMockedHttpMessageHandler(expectedUri, HttpMethod.Post, HttpStatusCode.OK, jsonResponse);
            HttpService httpService = HttpServiceFixtures.InjectMockToHttpService(httpMessageHandlerMock);

            // Act
            var result = await httpService.PostAsync<DummyDto>(expectedUri, payloadExpectedData);

            // Assert
            Assert.IsNotNull(result);
            Assert.That(result.Id, Is.EqualTo(returnExpectedData.Id));
            Assert.That(result.Name, Is.EqualTo(returnExpectedData.Name));

            httpMessageHandlerMock.Protected().Verify(
                "SendAsync",
                Times.Once(),
                ItExpr.Is<HttpRequestMessage>(req =>
                    req.Method == HttpMethod.Post &&
                    req.RequestUri.ToString().Contains(expectedUri) &&
                    req.Content.ReadAsStringAsync().Result == JsonConvert.SerializeObject(payloadExpectedData)),
                ItExpr.IsAny<CancellationToken>()
            );
        }

        [Test]
        public async Task PatchAsync_SendsDataAndReturnsDeserializedObjectOnSuccess()
        {
            // Arrange
            var expectedUri = "test/patch";
            var payloadExpectedData = new DummyDto { Id = 1, Name = "Payload" };
            var returnExpectedData = new DummyDto { Id = 2, Name = "ReturnValue" };
            var jsonResponse = JsonConvert.SerializeObject(returnExpectedData);
            Mock<HttpMessageHandler> httpMessageHandlerMock = HttpServiceFixtures.GetMockedHttpMessageHandler(expectedUri, HttpMethod.Patch, HttpStatusCode.OK, jsonResponse);
            HttpService httpService = HttpServiceFixtures.InjectMockToHttpService(httpMessageHandlerMock);

            // Act
            var result = await httpService.PatchAsync<DummyDto>(expectedUri, payloadExpectedData);

            // Assert
            Assert.IsNotNull(result);
            Assert.That(result.Id, Is.EqualTo(returnExpectedData.Id));
            Assert.That(result.Name, Is.EqualTo(returnExpectedData.Name));

            httpMessageHandlerMock.Protected().Verify(
                "SendAsync",
                Times.Once(),
                ItExpr.Is<HttpRequestMessage>(req =>
                    req.Method == HttpMethod.Patch &&
                    req.RequestUri.ToString().Contains(expectedUri) &&
                    req.Content.ReadAsStringAsync().Result == JsonConvert.SerializeObject(payloadExpectedData)),
                ItExpr.IsAny<CancellationToken>()
            );
        }

        [Test]
        public async Task DeleteAsync_MakesRequestAndHandlesResponseSuccessfully()
        {
            // Arrange
            var expectedUri = "test/delete";
            var expectedData = new DummyDto { Name = "DeleteData" };
            var jsonResponse = JsonConvert.SerializeObject(expectedData);
            Mock<HttpMessageHandler> httpMessageHandlerMock = HttpServiceFixtures.GetMockedHttpMessageHandler(expectedUri, HttpMethod.Delete, HttpStatusCode.OK, jsonResponse);
            HttpService httpService = HttpServiceFixtures.InjectMockToHttpService(httpMessageHandlerMock);

            // Act
            var result = await httpService.DeleteAsync<DummyDto>(expectedUri);

            // Assert
            Assert.IsNotNull(result);
            Assert.That(result.Name, Is.EqualTo(expectedData.Name));

            httpMessageHandlerMock.Protected().Verify(
                "SendAsync",
                Times.Once(),
                ItExpr.Is<HttpRequestMessage>(req =>
                    req.Method == HttpMethod.Delete &&
                    req.RequestUri.ToString().Contains(expectedUri)),
                ItExpr.IsAny<CancellationToken>()
            );
        }

        [Test]
        public async Task GetAsync_ThrowsApiExceptionOnErrorResponse()
        {
            // Arrange
            var expectedUri = "test/notfound";
            var errorResponse = "{\"error\":\"Not Found\",\"message\":\"The requested resource was not found.\"}";
            var statusCode = HttpStatusCode.NotFound;

            Mock<HttpMessageHandler> httpMessageHandlerMock = HttpServiceFixtures.GetMockedHttpMessageHandler(expectedUri, HttpMethod.Get, statusCode, errorResponse);
            HttpService httpService = HttpServiceFixtures.InjectMockToHttpService(httpMessageHandlerMock);

            // Act & Assert
            ApiException exception = Assert.ThrowsAsync<ApiException>(async () => await httpService.GetAsync<object>(expectedUri));

            Assert.IsNotNull(exception);
            Assert.That(exception.Message, Is.EqualTo("Not Found: The requested resource was not found."));

            httpMessageHandlerMock.Protected().Verify(
                "SendAsync",
                Times.Once(),
                ItExpr.Is<HttpRequestMessage>(req =>
                    req.Method == HttpMethod.Get &&
                    req.RequestUri.ToString().Contains(expectedUri)),
                ItExpr.IsAny<CancellationToken>()
            );
        }
    }

    internal class EmptyDto {};

    public class DummyDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
    }
}