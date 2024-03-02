using BeGiftee.Source.Services.Network.DTO;

namespace BeGiftee.Source.Services.Network.Exceptions
{
    public class ApiException : Exception
    {
        public ApiErrorResponse ErrorResponse { get; private set; }

        public ApiException(ApiErrorResponse errorResponse)
            : base($"{errorResponse.Error}: {errorResponse.Message}")
        {
            ErrorResponse = errorResponse;
        }
    }
}
