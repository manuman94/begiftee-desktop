
namespace BeGiftee.Source.Services.Network.Dto
{
    public class AuthResponseDto
    {
        public required string access_token { get; set; }
        public required string refresh_token { get; set; }
    }
}
