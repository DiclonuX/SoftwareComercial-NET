using System.Text.Json;

namespace API.Middleware
{
    public class ResponseMiddleware
    {
        private readonly RequestDelegate _next;

        public ResponseMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task Invoke(HttpContext context)
        {
            var originalBodyStream = context.Response.Body;
            using var responseStream = new MemoryStream();
            context.Response.Body = responseStream;

            await _next(context);

            context.Response.Body = originalBodyStream;

            var response = new
            {
                success = context.Response.StatusCode >= 200 && context.Response.StatusCode < 300,
                statusCode = context.Response.StatusCode,
                message = GetMessageForStatusCode(context.Response.StatusCode),
                data = responseStream.Length > 0 ? await ReadResponseBody(responseStream) : null
            };

            context.Response.ContentType = "application/json";
            await context.Response.WriteAsync(JsonSerializer.Serialize(response));
        }

        private async Task<string> ReadResponseBody(Stream responseStream)
        {
            responseStream.Seek(0, SeekOrigin.Begin);
            using var reader = new StreamReader(responseStream);
            return await reader.ReadToEndAsync();
        }

        private string GetMessageForStatusCode(int statusCode)
        {
            return statusCode switch
            {
                200 => "Operación exitosa",
                201 => "Recurso creado con éxito",
                400 => "Solicitud incorrecta",
                401 => "No autorizado",
                403 => "Prohibido",
                404 => "No encontrado",
                500 => "Error interno del servidor",
                _ => "Respuesta no especificada"
            };
        }
    }
}
