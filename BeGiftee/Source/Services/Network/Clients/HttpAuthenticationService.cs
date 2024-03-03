
using BeGiftee.Source.Services.Network.DTO;

namespace BeGiftee.Source.Services.Network.Clients
{
    public class HttpAuthenticationService : IAuthenticationService
    {
        private readonly HttpService _httpService;
        private string _accessToken;
        private string _refreshToken;

        public HttpAuthenticationService(HttpService httpService)
        {
            _httpService = httpService;
            _accessToken = string.Empty;
            _refreshToken = string.Empty;
        }

        public bool isLoggedIn()
        {
            return _accessToken != string.Empty;
        }

        public async Task<bool> Login(string username, string password)
        {
            var loginData = new { username, password };
            var loginResponse = await _httpService.PostAsync<AuthResponseDto>("auth/login", loginData);

            if (loginResponse != null && !string.IsNullOrEmpty(loginResponse.AccessToken))
            {
                _accessToken = loginResponse.AccessToken;
                _refreshToken = loginResponse.RefreshToken;
                _httpService.SetJwtToken(_accessToken);
                return true;
            }

            return false;
        }

        public async Task<bool> Register(string email, string username, string password)
        {
            var registerData = new { email, username, password };
            var registerResponse = await _httpService.PostAsync<AuthResponseDto>("auth/register", registerData);

            if (registerResponse != null && !string.IsNullOrEmpty(registerResponse.AccessToken))
            {
                _accessToken = registerResponse.AccessToken;
                _refreshToken = registerResponse.RefreshToken;
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
    }
}
