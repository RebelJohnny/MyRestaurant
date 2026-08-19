using Microsoft.AspNetCore.Http;

namespace MyRestaurant.Framework.Exceptions
{
    public sealed record Error
    {
        public required string Title { get; init; }
        public string? Message { get; init; } = null;
        public int StatusCode { get; init; } = StatusCodes.Status400BadRequest;
        public string InnerExceptionMessage { get; init; } = string.Empty;

        public static implicit operator RichException(Error error)
        {
            return new RichException(error.Title, error.Message, error.StatusCode);
        }
        public static readonly Error UnAuthorized = new()
        {
            Title = ExceptionMessages.UnAuthorized_Title,
            Message = ExceptionMessages.UnAuthorized_Description,
            StatusCode = StatusCodes.Status401Unauthorized
        };
        public static readonly Error Forbidden = new()
        {
            Title = ExceptionMessages.Forbidden_Title,
            Message = ExceptionMessages.Forbidden_Description,
            StatusCode = StatusCodes.Status403Forbidden
        };
        public static readonly Error NotFound = new()
        {
            Title = ExceptionMessages.NotFound_Title, 
            Message= ExceptionMessages.NotFound_Description,
            StatusCode = StatusCodes.Status404NotFound 
        };
    }
}
