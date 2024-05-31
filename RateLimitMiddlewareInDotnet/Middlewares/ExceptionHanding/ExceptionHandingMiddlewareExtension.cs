namespace RateLimitMiddlewareInDotnet.Middlewares.ExceptionHanding;

// UseGlobalExceptionHandling
public static class ExceptionHandingMiddlewareExtension
{
    public static IApplicationBuilder UseGlobalExceptionHandling(this IApplicationBuilder builder)
    {
        return builder.UseMiddleware<ErrorHandlingMiddleware>();
    }
}