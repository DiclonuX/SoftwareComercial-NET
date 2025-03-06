using System.Diagnostics;
using System.Text.Json;
using Serilog;
using Microsoft.AspNetCore.Http;

namespace API.Middleware
{
    public class LoggingMiddleware
    {
        private readonly RequestDelegate _next;

        public LoggingMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task Invoke(HttpContext context)
        {
            var stopwatch = Stopwatch.StartNew();

            // Capturar información de la solicitud
            var request = context.Request;
            var requestBody = request.ContentLength > 0 ? await ReadRequestBody(request) : null;

            Log.Information("Solicitud: {Method} {Path} | Usuario: {User} | Body: {Body}",
                request.Method, request.Path, context.User.Identity?.Name ?? "Anónimo", requestBody);

            await _next(context);

            stopwatch.Stop();

            // Capturar respuesta
            Log.Information("Respuesta: {StatusCode} | Tiempo: {ElapsedMilliseconds}ms",
                context.Response.StatusCode, stopwatch.ElapsedMilliseconds);
        }

        private async Task<string> ReadRequestBody(HttpRequest request)
        {
            request.EnableBuffering();
            using var reader = new StreamReader(request.Body, leaveOpen: true);
            var body = await reader.ReadToEndAsync();
            request.Body.Position = 0;
            return body;
        }
    }
}
