using System.Net;
using System.Text.Json;

namespace API.Middleware
{
    public class ErrorHandlingMiddleware
    {
        private readonly RequestDelegate _next;
        public ErrorHandlingMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (Exception ex)
            {
                await HandledExceptionAsync(context, ex);
            }
        }

        private static Task HandledExceptionAsync(HttpContext context, Exception exception)
        {
            var code = HttpStatusCode.InternalServerError;
            var result = JsonSerializer.Serialize(new { error = exception.Message, stackTrace = exception.StackTrace });
            context.Response.ContentType = "application/json"; ;
            context.Response.StatusCode = (int)code;
            return context.Response.WriteAsync(result);
        }
    }
}
