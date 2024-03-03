using BeGiftee.Source.Services.Network.Dto.Generic;

namespace BeGiftee.Source.Services.Network.Exceptions
{
    public class ApiException : Exception
    {
        public ApiErrorResponseDto ErrorResponse { get; private set; }

        public ApiException(ApiErrorResponseDto errorResponse)
            : base($"{errorResponse.Error}: {errorResponse.Message}")
        {
            ErrorResponse = errorResponse;
        }
    }
}
