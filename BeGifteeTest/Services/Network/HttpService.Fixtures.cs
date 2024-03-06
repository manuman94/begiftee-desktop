using Moq.Protected;
using Moq;
using System.Net;
using BeGiftee.Source.Services.Network;

namespace BeGifteeTest.Services.Network
{
    public class HttpServiceFixtures
    {
        public static HttpService InjectMockToHttpService(Mock<HttpMessageHandler> handler)
        {
            var httpClient = new HttpClient(handler.Object)
            {
                BaseAddress = new Uri("http://localhost:3000"), // TODO create data source for base url, now has to be aligned with HttpService
            };

            return new HttpService(httpClient);
        }
        public static Mock<HttpMessageHandler> GetMockedHttpMessageHandler(string expectedUri, HttpMethod method, HttpStatusCode mockedResponseStatusCode, string mockedResponseContent)
        {
            var mockHandler = new Mock<HttpMessageHandler>();

            mockHandler.Protected()
                .Setup<Task<HttpResponseMessage>>(
                    "SendAsync",
                    ItExpr.Is<HttpRequestMessage>(req =>
                        req.Method == method &&
                        req.RequestUri.ToString().Contains(expectedUri)),
                    ItExpr.IsAny<CancellationToken>()
                )
                .ReturnsAsync(new HttpResponseMessage
                {
                    StatusCode = mockedResponseStatusCode,
                    Content = new StringContent(mockedResponseContent)
                })
                .Verifiable();
            return mockHandler;
        }
    }
}
