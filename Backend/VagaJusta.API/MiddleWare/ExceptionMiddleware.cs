using FluentValidation;
using System.Text.Json;
using VagaJusta.Application.Exceptions;
using VagaJusta.Domain.Exceptions;

namespace VagaJusta.API.MiddleWare
{
    public class ExceptionMiddleware(RequestDelegate next, ILogger<ExceptionMiddleware> logger, IHostEnvironment env)
    {
        private readonly RequestDelegate _next = next;
        private readonly ILogger<ExceptionMiddleware> _logger = logger;
        private readonly IHostEnvironment _env = env;

        public async Task InvokeAsync(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (Exception e)
            {
                var error = MappedException(e);

                _logger.Log(
                    error.LogLevel,
                    e,
                    "Falha na requisição {Method} {Path}. Status: {StatusCode}. TraceId: {TraceId}",
                    context.Request.Method,
                    context.Request.Path,
                    error.StatusCode,
                    context.TraceIdentifier);

                context.Response.ContentType = "application/json; charset=utf-8";
                context.Response.StatusCode = error.StatusCode;

                var body = new
                {
                    title = error.Title,
                    traceId = context.TraceIdentifier,
                    errors = error.Errors,
                    instance = context.Request.Path.ToString()
                };

                await context.Response.WriteAsync(JsonSerializer.Serialize(body));
            }
        }

        private static ExceptionMapped MappedException(Exception e) => e switch
        {
            ValidationException ve => new(StatusCodes.Status400BadRequest, "Erros de validação.", LogLevel.Warning, ve.Errors.Select(x => x.ErrorMessage)),
            DomainException => new(StatusCodes.Status400BadRequest, e.Message, LogLevel.Warning),
            NotFoundException => new(StatusCodes.Status404NotFound, e.Message, LogLevel.Warning),
            UnauthorizedAccessException => new(StatusCodes.Status401Unauthorized, e.Message, LogLevel.Warning),
            _ => new(StatusCodes.Status500InternalServerError, "Erro interno do servidor.", LogLevel.Error)
        };

        private record ExceptionMapped(int StatusCode, string Title, LogLevel LogLevel, IEnumerable<string> Errors = default!);
    }
}
