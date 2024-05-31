using System.Net;

namespace RateLimitMiddlewareInDotnet.Middlewares.ExceptionHanding;

public static class ExceptionHandler
{
    public static async Task HandleRateLimitExceeded(HttpContext context, string exMessage)
    {
        context.Response.ContentType = "application/json";
        context.Response.StatusCode = (int)HttpStatusCode.TooManyRequests;

        Console.WriteLine(exMessage);

        await context.Response.WriteAsync(exMessage);
    }

    public static async Task HandleGeneral(HttpContext context, Exception ex)
    {
        context.Response.ContentType = "application/json";
        context.Response.StatusCode = (int)HttpStatusCode.BadRequest;

        Console.WriteLine(ex.Message);

        await context.Response.WriteAsync(ex.Message);
    }
}
