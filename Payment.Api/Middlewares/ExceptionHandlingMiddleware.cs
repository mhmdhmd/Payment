using System.Net;
using Newtonsoft.Json;
using Payment.Application.ViewModels.Base;
using Payment.Infrastructure.Exceptions;

namespace Payment.Api.Middlewares;

public class ExceptionHandlingMiddleware
{
    private readonly RequestDelegate _next;
    private readonly ILogger<ExceptionHandlingMiddleware> _logger;

    public ExceptionHandlingMiddleware(RequestDelegate next, ILogger<ExceptionHandlingMiddleware> logger)
    {
        _next = next;
        _logger = logger;
    }

    public async Task InvokeAsync(HttpContext context)
    {
        try
        {
            await _next(context);
        }
        catch (Exception e)
        {
            _logger.LogError(e, "An exception occurred.");
            await HandleExceptionAsync(context, e);
        }
    }

    private static Task HandleExceptionAsync(HttpContext context, Exception exception)
    {
        HttpStatusCode statusCode;

        switch (exception)
        {
            case ArgumentException:
            case PaymentException:
                statusCode = HttpStatusCode.BadRequest;
                break;
            default:
                statusCode = HttpStatusCode.InternalServerError;
                break;
        }

        var result = new ResultBase
        {
            Success = false,
            ErrorMessage = exception.Message
        };

        var responseBody = JsonConvert.SerializeObject(result, Formatting.Indented);
        context.Response.Clear();
        context.Response.ContentType = "application/json";
        context.Response.StatusCode = (int)statusCode;

        return context.Response.WriteAsync(responseBody);
    }
}