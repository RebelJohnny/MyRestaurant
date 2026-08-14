using Microsoft.AspNetCore.Http;

namespace MyRestaurant.Framework.Exceptions
{
    public class RichException(string title, string? message = null, int statusCode = StatusCodes.Status400BadRequest) : Exception(message)
    {
        public string Title { get; private set; } = title;
        public int StatusCode { get; private set; } = statusCode;
    }
}
