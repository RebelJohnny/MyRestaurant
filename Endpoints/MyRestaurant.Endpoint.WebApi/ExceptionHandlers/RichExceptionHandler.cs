using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using MyRestaurant.Framework.Exceptions;

namespace MyRestaurant.Endpoint.WebApi.ExceptionHandlers
{
    internal sealed class RichExceptionHandler(IProblemDetailsService problemDetailsService, ILogger<RichExceptionHandler> logger) : IExceptionHandler
    {
        public async ValueTask<bool> TryHandleAsync(HttpContext httpContext, Exception exception, CancellationToken cancellationToken)
        {
            if (exception is not RichException richException)
            {
                return false;
            }

            logger.LogError(exception, "Unhandled exception occurred");

            httpContext.Response.StatusCode = richException.StatusCode;

            var context = new ProblemDetailsContext
            {
                HttpContext = httpContext,
                Exception = exception,
                ProblemDetails = new ProblemDetails
                {
                    Title = richException.Title,
                    Status = richException.StatusCode,
                    Detail = richException.Message
                }
            };

            return await problemDetailsService.TryWriteAsync(context);
        }
    }
}
