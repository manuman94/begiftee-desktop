
namespace BeGiftee.Source.Services.Network.Dto
{
    public class AuthResponseDto
    {
        public required string AccessToken { get; set; }
        public required string RefreshToken { get; set; }
    }
}
