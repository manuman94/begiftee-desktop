namespace BeGiftee.Source.Services.Network.Dto.Generic
{
    public class ApiErrorResponseDto
    {
        public required int StatusCode { get; set; }
        public required string Error { get; set; }
        public required object Message { get; set; } // TODO Server sometimes returns string and sometimes string[]
        public required double Timestamp { get; set; }
    }
}