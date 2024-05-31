using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.RateLimiting;
using RateLimitMiddlewareInDotnet.Models;

namespace RateLimitMiddlewareInDotnet.Controllers;
[ApiController]
[Route("[controller]")]
public class WeatherForecastController : ControllerBase
{
    private static object _counterLock = new();
    private static int _counter = 1;

    private static readonly string[] Summaries = new[]
    {
        "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
    };

    private readonly ILogger<WeatherForecastController> _logger;

    public WeatherForecastController(ILogger<WeatherForecastController> logger)
    {
        _logger = logger;
    }

    [HttpGet(Name = "GetWeatherForecast")]
    [EnableRateLimiting("fixed-by-ip")]
    public IEnumerable<WeatherForecast> Get()
    {
        int currentCount = default;

        lock (_counterLock)
        {
            currentCount = _counter++;
            Console.WriteLine("CurrentCount: " + currentCount);
        }

        Console.WriteLine("Count: " + currentCount + " finish");

        return Enumerable.Range(1, 5).Select(index => new WeatherForecast
        {
            Date = DateOnly.FromDateTime(DateTime.Now.AddDays(index)),
            TemperatureC = Random.Shared.Next(-20, 55),
            Summary = Summaries[Random.Shared.Next(Summaries.Length)]
        })
        .ToArray();
    }
}
