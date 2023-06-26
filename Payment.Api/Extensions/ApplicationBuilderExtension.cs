using Payment.Api.Middlewares;

namespace Payment.Api.Extensions;

public static class ApplicationBuilderExtension
{
    public static IApplicationBuilder UseCustomExceptionHandler(this IApplicationBuilder app)
    {
        app.UseMiddleware<ExceptionHandlingMiddleware>();
        return app;
    }
}