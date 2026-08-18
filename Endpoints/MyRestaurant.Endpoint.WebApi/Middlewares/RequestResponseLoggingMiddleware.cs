using System.Diagnostics;
using System.Text;

namespace MyRestaurant.Endpoint.WebApi.Middlewares
{
    public sealed class RequestResponseLoggingMiddleware(RequestDelegate next, IWebHostEnvironment environment)
    {
        public async Task InvokeAsync(HttpContext context)
        {
            var stopwatch = Stopwatch.StartNew();

            Exception? exception = null;

            // We only need to buffer the response if something goes wrong.
            var originalResponseBody = context.Response.Body;

            await using var responseBody = new MemoryStream();
            context.Response.Body = responseBody;

            try
            {
                await next(context);
            }
            catch (Exception ex)
            {
                exception = ex;
                throw;
            }
            finally
            {
                stopwatch.Stop();

                var statusCode = context.Response.StatusCode;

                if (exception is not null || statusCode >= 400)
                {
                    await LogFailedRequestAsync(
                        context,
                        responseBody,
                        stopwatch.ElapsedMilliseconds,
                        exception);
                }
                else
                {
                    await LogSuccessfulRequestAsync(
                        context,
                        stopwatch.ElapsedMilliseconds);
                }

                responseBody.Position = 0;
                await responseBody.CopyToAsync(originalResponseBody);

                context.Response.Body = originalResponseBody;
            }
        }

        private async Task LogSuccessfulRequestAsync(
            HttpContext context,
            long elapsedMilliseconds)
        {
            var message =
                $"[{DateTime.Now:yyyy-MM-dd HH:mm:ss.fff}] " +
                $"{context.Request.Method} " +
                $"{context.Request.Path}{context.Request.QueryString} " +
                $"-> {context.Response.StatusCode} " +
                $"({elapsedMilliseconds} ms)";

            await WriteLogAsync(message);
        }

        private async Task LogFailedRequestAsync(
            HttpContext context,
            Stream responseBody,
            long elapsedMilliseconds,
            Exception? exception)
        {
            var requestBody = await ReadRequestBodyAsync(context.Request);
            var responseBodyText = await ReadResponseBodyAsync(responseBody);

            var log = new StringBuilder();

            log.AppendLine("==================================================");
            log.AppendLine(
                $"Timestamp : {DateTime.Now:yyyy-MM-dd HH:mm:ss.fff}");

            log.AppendLine(
                $"Request   : {context.Request.Method} " +
                $"{context.Request.Path}{context.Request.QueryString}");

            log.AppendLine($"Status    : {context.Response.StatusCode}");
            log.AppendLine($"Duration  : {elapsedMilliseconds} ms");

            log.AppendLine();
            log.AppendLine("Request Headers:");

            foreach (var header in context.Request.Headers)
            {
                log.AppendLine($"{header.Key}: {header.Value}");
            }

            log.AppendLine();
            log.AppendLine("Request Body:");
            log.AppendLine(requestBody);

            log.AppendLine();
            log.AppendLine("Response Headers:");

            foreach (var header in context.Response.Headers)
            {
                log.AppendLine($"{header.Key}: {header.Value}");
            }

            log.AppendLine();
            log.AppendLine("Response Body:");
            log.AppendLine(responseBodyText);

            if (exception is not null)
            {
                log.AppendLine();
                log.AppendLine("Exception:");
                log.AppendLine(exception.ToString());
            }

            log.AppendLine();
            log.AppendLine("==================================================");
            log.AppendLine();

            await WriteLogAsync(log.ToString());
        }

        private static async Task<string> ReadRequestBodyAsync(
            HttpRequest request)
        {
            if (request.ContentLength is null or 0)
                return string.Empty;

            request.EnableBuffering();

            request.Body.Position = 0;

            using var reader = new StreamReader(
                request.Body,
                Encoding.UTF8,
                leaveOpen: true);

            var body = await reader.ReadToEndAsync();

            request.Body.Position = 0;

            return body;
        }

        private static async Task<string> ReadResponseBodyAsync(
            Stream responseBody)
        {
            responseBody.Position = 0;

            using var reader = new StreamReader(
                responseBody,
                Encoding.UTF8,
                leaveOpen: true);

            var body = await reader.ReadToEndAsync();

            responseBody.Position = 0;

            return body;
        }

        private async Task WriteLogAsync(string message)
        {
            var logsDirectory = Path.Combine(
                environment.ContentRootPath,
                "Logs");

            Directory.CreateDirectory(logsDirectory);

            var filePath = Path.Combine(
                logsDirectory,
                $"{DateTime.Now:yyyy-MM-dd}.txt");

            await File.AppendAllTextAsync(
                filePath,
                message);
        }
    }

}
