
using BeGiftee.Source.Services.Api;
using BeGiftee.Source.Services.Network.Dto;
using BeGiftee.Source.Services.Network.Dto.Auth;

namespace BeGiftee.Source.Services.Network.Clients
{
    public class HttpAuthenticationService(HttpService httpService) : IAuthenticationService
    {
        private readonly HttpService _httpService = httpService;
        private string _accessToken = string.Empty;
        private string _refreshToken = string.Empty;

        public bool isLoggedIn()
        {
            return _accessToken != string.Empty;
        }

        public async Task<bool> Login(string username, string password)
        {
            var loginData = new { username, password };
            var loginResponse = await _httpService.PostAsync<AuthResponseDto>(ApiEndpoints.Login, loginData);

            if (loginResponse != null && !string.IsNullOrEmpty(loginResponse.access_token))
            {
                _accessToken = loginResponse.access_token;
                _refreshToken = loginResponse.refresh_token;
                _httpService.SetJwtToken(_accessToken);
                return true;
            }

            return false;
        }

        public async Task<bool> Register(string email, string username, string password)
        {
            var registerData = new { email, username, password };
            var registerResponse = await _httpService.PostAsync<AuthResponseDto>(ApiEndpoints.Register, registerData);

            if (registerResponse != null && !string.IsNullOrEmpty(registerResponse.access_token))
            {
                _accessToken = registerResponse.access_token;
                _refreshToken = registerResponse.refresh_token;
                _httpService.SetJwtToken(_accessToken);
                return true;
            }

            return false;
        }

        public void Logout()
        {
            _accessToken = string.Empty;
            _refreshToken = string.Empty;
            _httpService.SetJwtToken(string.Empty);
        }

        public async Task<bool> RecoverPassword(string email)
        {
            var recoverPasswordData = new { email };
            await _httpService.PostAsync<RecoverResponse>(ApiEndpoints.RecoverPassword, recoverPasswordData);
            return true;
        }
    }
}
