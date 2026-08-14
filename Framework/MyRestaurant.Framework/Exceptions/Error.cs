using Microsoft.AspNetCore.Http;

namespace MyRestaurant.Framework.Exceptions
{
    public sealed record Error(string Title, string? Message = null, int StatusCode = StatusCodes.Status400BadRequest)
    {
        public static implicit operator RichException(Error error)
        {
            return new RichException(error.Title, error.Message, error.StatusCode);
        }
        public static readonly Error UnAuthorized = new(ExceptionMessages.UnAuthorized_Title, ExceptionMessages.UnAuthorized_Description, StatusCodes.Status401Unauthorized);
        public static readonly Error Forbidden = new(ExceptionMessages.Forbidden_Title, ExceptionMessages.Forbidden_Description, StatusCodes.Status403Forbidden);
        public static readonly Error NotFound = new(ExceptionMessages.NotFound_Title, ExceptionMessages.NotFound_Description, StatusCodes.Status404NotFound);
    }
}
