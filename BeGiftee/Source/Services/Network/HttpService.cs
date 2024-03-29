﻿using System.Net.Http.Headers;
using BeGiftee.Source.Services.Network.Exceptions;
using Newtonsoft.Json;
using BeGiftee.Source.Services.Network.Dto.Generic;
using System.Net.Http;

namespace BeGiftee.Source.Services.Network
{
    public class HttpService
    {
        private readonly HttpClient _httpClient;
        private string _jwtToken; // TODO Do I want to store the raw value here?
        private readonly string _baseURL = "http://localhost:3000"; // TODO create environment file to store different environment endpoints

        public HttpService(HttpClient httpClient)
        {
            _httpClient = httpClient ?? throw new ArgumentNullException(nameof(httpClient));
            _httpClient.BaseAddress = new Uri(_baseURL);
            _jwtToken = string.Empty;
        }

        public void SetJwtToken(string jwtToken)
        {
            _jwtToken = jwtToken;
            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", jwtToken);
        }

        private async Task<T?> PerformRequestAsync<T>(string method, string uri, object? data = null)
        {
            var content = new StringContent(string.Empty);
            if ( data != null )
                content = new StringContent(JsonConvert.SerializeObject(data), System.Text.Encoding.UTF8, "application/json");
            var request = new HttpRequestMessage(new HttpMethod(method), uri) { Content = content };
            var response = await _httpClient.SendAsync(request);
            if (!response.IsSuccessStatusCode)
            {
                throw await HandleNonSuccessResponse(response);
            }
            var json = await response.Content.ReadAsStringAsync();
            return JsonConvert.DeserializeObject<T?>(json);
        }

        private static async Task<Exception> HandleNonSuccessResponse(HttpResponseMessage response)
        {
            var content = await response.Content.ReadAsStringAsync();
            try
            {
                var errorResponse = JsonConvert.DeserializeObject<ApiErrorResponseDto>(content);
                if (errorResponse != null)
                {
                    return new ApiException(errorResponse);
                }
            }
            catch (JsonException e)
            {
                throw new JsonException("Failed when trying to deserialize error message from the server", e);
            }

            return new Exception("An error occurred while processing the request.");
        }

        public async Task<T?> GetAsync<T>(string uri)
        {
            return await this.PerformRequestAsync<T>(HttpMethod.Get.ToString(), uri);
        }

        public async Task<T?> PostAsync<T>(string uri, object data)
        {
            return await this.PerformRequestAsync<T>(HttpMethod.Post.ToString(), uri, data);
        }

        public async Task<T?> PatchAsync<T>(string uri, object data)
        {
            return await this.PerformRequestAsync<T>(HttpMethod.Patch.ToString(), uri, data);
        }

        public async Task<T?> DeleteAsync<T>(string uri)
        {
            return await this.PerformRequestAsync<T>(HttpMethod.Delete.ToString(), uri);
        }
    }
}
