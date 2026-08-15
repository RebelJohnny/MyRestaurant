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

            logger.LogError(exception, "richException occurred");

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

            //var result = await problemDetailsService.TryWriteAsync(context);

            //logger.LogInformation(
            //    "ProblemDetails written: {Result}, Status: {StatusCode}",
            //    result,
            //    httpContext.Response.StatusCode);

            //return result;
            await httpContext.Response.WriteAsJsonAsync(
            new
            {
                title = richException.Title,
                status = richException.StatusCode,
                detail = richException.Message
            },
            cancellationToken);

            return true;
        }
    }
}
