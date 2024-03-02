using System.Net.Http.Headers;
using BeGiftee.Source.Services.Network.DTO;
using BeGiftee.Source.Services.Network.Exceptions;
using Newtonsoft.Json;

namespace BeGiftee.Source.Services.Network
{
    public class HttpService
    {
        private readonly HttpClient _httpClient;
        private string _jwtToken;
        private readonly string _baseURL = "http://localhost:3000";

        public HttpService()
        {
            _httpClient = new HttpClient
            {
                BaseAddress = new Uri(_baseURL)
            };
            _jwtToken = "";
        }

        public void SetJwtToken(string jwtToken)
        {
            _jwtToken = jwtToken;
            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", jwtToken);
        }

        public async Task<T?> GetAsync<T>(string uri)
        {
            var response = await _httpClient.GetAsync(uri);
            var json = await response.Content.ReadAsStringAsync();
            return JsonConvert.DeserializeObject<T>(json);
        }

        private async Task<T?> NonGetAsync<T>(string method, string uri, object? data = null)
        {
            var content = new StringContent("");
            if ( data != null )
            {
                content = new StringContent(JsonConvert.SerializeObject(data), System.Text.Encoding.UTF8, "application/json");
            }
            var request = new HttpRequestMessage(new HttpMethod(method), uri) { Content = content };
            var response = await _httpClient.SendAsync(request);
            var json = await response.Content.ReadAsStringAsync();
            return JsonConvert.DeserializeObject<T?>(json);
        }

        private async Task<Exception> HandleNonSuccessResponse(HttpRequestException e, HttpResponseMessage response)
        {
            var content = await response.Content.ReadAsStringAsync();
            try
            {
                var errorResponse = JsonConvert.DeserializeObject<ApiErrorResponse>(content);
                if (errorResponse != null)
                {
                    return new ApiException(errorResponse);
                }
            }
            catch (JsonException)
            {
                throw new JsonException("Failed when trying to deserialize error message from the server", e);
            }

            return new Exception("An error occurred while processing the request.", e);
        }

        public async Task<T?> PostAsync<T>(string uri, object data)
        {
            return await this.NonGetAsync<T>("POST", uri, data);
        }

        public async Task<T?> PatchAsync<T>(string uri, object data)
        {
            return await this.NonGetAsync<T>("POST", uri, data);
        }

        public async Task DeleteAsync(string uri)
        {
            await _httpClient.DeleteAsync(uri);
        }
    }
}
