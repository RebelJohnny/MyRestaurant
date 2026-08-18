using System.Diagnostics;
using System.Net;
using System.Text;

namespace MyRestaurant.Endpoint.WebApi.Middlewares
{
    public class RequestResponseLoggingMiddleware(
        RequestDelegate next,
        ILogger<RequestResponseLoggingMiddleware> logger)
    {
        public async Task Invoke(HttpContext context)
        {
            Stopwatch stopwatch = Stopwatch.StartNew();

            // Capture the response body
            Stream originalResponseBody = context.Response.Body;
            await using MemoryStream responseBodyStream = new MemoryStream();
            context.Response.Body = responseBodyStream;

            try
            {
                string requestBodyContent = await ReadRequestBodyAsync(context);
                string userName =
                    (context.User.Identity?.IsAuthenticated ?? false)
                        ? context.User.Identity.Name ?? "Unknown"
                        : "Anonymous";

                string clientIp = GetClientIp(context);

                string logEntry =
                    $"[{DateTime.Now:yyyy-MM-dd HH:mm:ss}] " +
                    $"Request: {context.Request.Method} " +
                    $"Path: {context.Request.Path} " +
                    $"User: {userName} " +
                    $"IP: {clientIp} " +
                    $"Body: {requestBodyContent}";

                await LogToFileAsync(logEntry);
                logger.LogInformation(logEntry);

                await next(context);
            }
            catch (Exception ex)
            {
                logger.LogError(
                    ex,
                    "An unhandled exception occurred while processing the request.");

                await LogToFileAsync(
                    $"[{DateTime.Now:yyyy-MM-dd HH:mm:ss}] ERROR: {ex}");

                throw;
            }
            finally
            {
                stopwatch.Stop();

                // Make sure we're at the beginning before reading
                responseBodyStream.Position = 0;

                string responseBody = string.Empty;

                try
                {
                    using StreamReader reader = new StreamReader(
                        responseBodyStream,
                        Encoding.UTF8,
                        detectEncodingFromByteOrderMarks: true,
                        leaveOpen: true);

                    responseBody = await reader.ReadToEndAsync();
                }
                catch (Exception ex)
                {
                    logger.LogError(
                        ex,
                        "Failed to read captured response body.");
                }

                // Restore the original response stream
                responseBodyStream.Position = 0;
                await responseBodyStream.CopyToAsync(originalResponseBody);
                context.Response.Body = originalResponseBody;

                string responseLog;

                if (context.Response.StatusCode >= 400)
                {
                    responseLog =
                        $"[{DateTime.Now:yyyy-MM-dd HH:mm:ss}] " +
                        $"Finished request with status {context.Response.StatusCode} " +
                        $"in {stopwatch.ElapsedMilliseconds}ms " +
                        $"Response Body: {responseBody}";
                }
                else
                {
                    responseLog =
                        $"[{DateTime.Now:yyyy-MM-dd HH:mm:ss}] " +
                        $"Finished request with status {context.Response.StatusCode} " +
                        $"in {stopwatch.ElapsedMilliseconds}ms";
                }

                await LogToFileAsync(responseLog);
                logger.LogInformation(responseLog);
            }
        }

        private async Task<string> ReadRequestBodyAsync(HttpContext context)
        {
            context.Request.EnableBuffering();

            using StreamReader reader = new StreamReader(
                context.Request.Body,
                Encoding.UTF8,
                detectEncodingFromByteOrderMarks: true,
                leaveOpen: true);

            string requestBody = await reader.ReadToEndAsync();

            context.Request.Body.Position = 0;

            return !string.IsNullOrEmpty(requestBody)
                ? requestBody
                : "{}";
        }

        private string GetClientIp(HttpContext context)
        {
            string? clientIp =
                context.Request.Headers["X-Forwarded-For"].FirstOrDefault();

            if (string.IsNullOrWhiteSpace(clientIp))
            {
                IPAddress? remoteIp = context.Connection.RemoteIpAddress;

                if (remoteIp != null)
                {
                    if (remoteIp.IsIPv4MappedToIPv6)
                    {
                        clientIp = remoteIp.MapToIPv4().ToString();
                    }
                    else
                    {
                        clientIp = remoteIp.ToString();
                    }

                    if (clientIp == "::1")
                    {
                        clientIp = "127.0.0.1";
                    }
                }
                else
                {
                    clientIp = "Unknown IP";
                }
            }

            if (clientIp == "127.0.0.1")
            {
                List<string> localIps = GetLocalNetworkIPs();

                clientIp =
                    $"127.0.0.1 (Local IPs: {string.Join(", ", localIps)})";
            }

            return clientIp;
        }

        private static List<string> GetLocalNetworkIPs()
        {
            try
            {
                return Dns.GetHostAddresses(Dns.GetHostName())
                    .Where(ip =>
                        ip.AddressFamily ==
                        System.Net.Sockets.AddressFamily.InterNetwork)
                    .Select(ip => ip.ToString())
                    .ToList();
            }
            catch
            {
                return ["Unable to retrieve local IPs"];
            }
        }

        private async Task LogToFileAsync(string logEntry)
        {
            try
            {
                string date = DateTime.Now.ToString("yyyy-MM-dd");

                string logDirectory = Path.Combine(
                    Directory.GetCurrentDirectory(),
                    "Logs",
                    date);

                if (!Directory.Exists(logDirectory))
                {
                    Directory.CreateDirectory(logDirectory);
                }

                string logFilePath = Path.Combine(
                    logDirectory,
                    "requests_log.txt");

                await File.AppendAllTextAsync(
                    logFilePath,
                    logEntry + Environment.NewLine);
            }
            catch (Exception ex)
            {
                logger.LogError(
                    "Failed to write log to file: {Message}",
                    ex.Message);
            }
        }
    }
}