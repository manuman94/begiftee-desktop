namespace BeGiftee.Source.Services.Network.Dto
{
    public class ApiErrorResponseDto
    {
        public required int StatusCode { get; set; }
        public required string Error { get; set; }
        public required string Message { get; set; }
        public required double Timestamp { get; set; }
    }
}
