using System.Net;
using System.Text.Json;
using System.ComponentModel.DataAnnotations;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;

namespace Backend.Mappers
{
    internal static class ExceptionMessageMapper
    {
        internal static (string responseMessage, HttpStatusCode responseStatusCode) GetExceptionDetails(Exception exception)
        {
            return exception switch
            {
                ArgumentNullException => ("A required argument was null.", HttpStatusCode.BadRequest),
                ArgumentException => ("Invalid argument.", HttpStatusCode.BadRequest),
                KeyNotFoundException => ("Resource not found.", HttpStatusCode.NotFound),
                InvalidOperationException => ("Invalid operation.", HttpStatusCode.BadRequest),
                UnauthorizedAccessException => ("Unauthorized.", HttpStatusCode.Unauthorized),
                NotImplementedException => ("Feature not implemented.", HttpStatusCode.NotImplemented),
                HttpRequestException => ("Request failed.", HttpStatusCode.BadGateway),
                TaskCanceledException => ("Request timed out.", HttpStatusCode.RequestTimeout),
                FormatException => ("Invalid format.", HttpStatusCode.BadRequest),
                JsonException => ("Malformed JSON.", HttpStatusCode.BadRequest),
                SqlException => ("Database error.", HttpStatusCode.InternalServerError),
                DbUpdateException => ("Failed to update database.", HttpStatusCode.Conflict),
                ValidationException => ("Validation failed.", HttpStatusCode.BadRequest),
                _ => ("An unexpected error occurred.", HttpStatusCode.InternalServerError),
            };
        }

        internal static string GetLogMessage(Exception exception)
        {
            return exception.Message ?? $"Exception of type {exception.GetType()} occurred.";
        }
    }
}