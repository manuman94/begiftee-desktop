
namespace BeGiftee.Source.Services.Network.DTO
{
    public class AuthResponseDto
    {
        public required string AccessToken { get; set; }
        public required string RefreshToken { get; set; }
    }
}
