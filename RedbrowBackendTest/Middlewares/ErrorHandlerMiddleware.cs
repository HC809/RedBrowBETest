using AutoMapper;
using Npgsql;
using System.Net;
using System.Text.Json;

namespace RedbrowBackendTest.Middlewares
{
    public class ErrorHandlerMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly Dictionary<Type, Func<Exception, (int, string)>> _exceptionHandlers;

        public ErrorHandlerMiddleware(RequestDelegate next)
        {
            _next = next;
            _exceptionHandlers = new Dictionary<Type, Func<Exception, (int, string)>>
            {
                { typeof(NpgsqlException), (e) => HandleNpgsqlException((NpgsqlException)e) },
                { typeof(ArgumentException), (e) => ((int)HttpStatusCode.BadRequest, e.Message) },
                { typeof(AutoMapperMappingException), (e) => ((int)HttpStatusCode.BadRequest, $"Automapper Error: {((AutoMapperMappingException)e).InnerException?.Message}") },
            };
        }

        public async Task Invoke(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (Exception error)
            {
                await HandleExceptionAsync(context, error);
            }
        }

        private async Task HandleExceptionAsync(HttpContext context, Exception error)
        {
            var response = context.Response;
            response.ContentType = "application/json";

            var statusCode = (int)HttpStatusCode.InternalServerError;
            var message = "An unexpected error occurred.";

            if (_exceptionHandlers.ContainsKey(error.GetType()))
            {
                (statusCode, message) = _exceptionHandlers[error.GetType()](error);
            }
            else
            {
                switch (error)
                {
                    case ApplicationException:
                        statusCode = (int)HttpStatusCode.BadRequest;
                        message = error.Message;
                        break;
                    case KeyNotFoundException:
                        statusCode = (int)HttpStatusCode.NotFound;
                        message = error.Message;
                        break;
                    default:
                        statusCode = (int)HttpStatusCode.InternalServerError;
                        break;
                }
            }

            response.StatusCode = statusCode;
            var result = JsonSerializer.Serialize(new { message });
            await response.WriteAsync(result);
        }

        private (int, string) HandleNpgsqlException(NpgsqlException npgsqlException)
        {
            var errorMapping = new Dictionary<string, (int, string)>
                {
                    { "22001", ((int)HttpStatusCode.BadRequest, "Database error: String data right truncation.") },
                    { "22P02", ((int)HttpStatusCode.BadRequest, "Database error: Invalid text representation.") },
                    { "23505", ((int)HttpStatusCode.Conflict, "Database error: Unique violation.") },
                    { "23503", ((int)HttpStatusCode.BadRequest, "Database error: Foreign key violation.") },
                    { "23502", ((int)HttpStatusCode.BadRequest, "Database error: NOT NULL violation.") },
                };

            if (errorMapping.TryGetValue(npgsqlException.SqlState!, out var errorDetails))
                return errorDetails;


            return ((int)HttpStatusCode.BadRequest, $"Database error: {npgsqlException.Message}");
        }
    }
}
