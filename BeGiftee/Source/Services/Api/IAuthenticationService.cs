namespace BeGiftee.Source.Services.Api
{
    public interface IAuthenticationService
    {
        bool isLoggedIn();
        Task<bool> Login(string username, string password);
        Task<bool> Register(string email, string username, string password);
        Task<bool> RecoverPassword(string email);
        void Logout();
    }
}
