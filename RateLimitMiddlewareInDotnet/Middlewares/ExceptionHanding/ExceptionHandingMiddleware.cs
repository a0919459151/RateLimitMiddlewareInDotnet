using System.Net;

namespace RateLimitMiddlewareInDotnet.Middlewares.ExceptionHanding;

public class ErrorHandlingMiddleware
{
    private readonly RequestDelegate next;

    public ErrorHandlingMiddleware(RequestDelegate next)
    {
        this.next = next;
    }

    public async Task Invoke(HttpContext context)
    {
        try
        {
            await next(context);

            // If status code is 429 (which is handle from dotnet build-in rate limit middleware),
            // handle to many request exception
            if (context.Response.StatusCode == (int)HttpStatusCode.TooManyRequests)
            {
                await ExceptionHandler.HandleRateLimitExceeded(context, "Rage limit exceed");
            }
        }
        catch (Exception ex)
        {
            await ExceptionHandler.HandleGeneral(context, ex);
        }
    }
}
