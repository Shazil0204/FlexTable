using System.Net;
using System.Text.Json;
using Backend.DTOs;
using Backend.Mappers;

namespace Backend.Middleware
{
    public class ExceptionMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<ExceptionMiddleware> _logger;

        public ExceptionMiddleware(RequestDelegate next, ILogger<ExceptionMiddleware> logger)
        {
            _next = next;
            _logger = logger;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            try
            {
                if (context.Request.Path.StartsWithSegments("/swagger") || context.Request.Path.StartsWithSegments("/openapi"))
                {
                    await _next(context).ConfigureAwait(false);
                    return;
                }

                await _next(context).ConfigureAwait(false);
            }
            catch (Exception ex)
            {
                string logMessage = ExceptionMessageMapper.GetLogMessage(ex);
                var (responseMessage, statusCode) = ExceptionMessageMapper.GetExceptionDetails(ex);

                _logger.LogError(ex, "An exception has occurred: {0}", logMessage);
                string details = $"Path: {context.Request.Path}, Method: {context.Request.Method}";
                string exceptionMessage = ex.Message; // Actual exception message
                await HandleExceptionAsync(context, exceptionMessage, statusCode, details, responseMessage).ConfigureAwait(false);
            }
        }

        private static Task HandleExceptionAsync(HttpContext context, string exceptionMessage, HttpStatusCode statusCode, string? details = null, string? mappedMessage = null)
        {
            context.Response.ContentType = "application/json";
            context.Response.StatusCode = (int)statusCode;

            FailureResponseDTO response = new FailureResponseDTO(exceptionMessage, details)
            {
                Exception = mappedMessage 
            };

            string responseJson = JsonSerializer.Serialize(response);
            return context.Response.WriteAsync(responseJson);
        }
    }
}