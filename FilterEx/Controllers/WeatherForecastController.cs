using Microsoft.AspNetCore.Mvc;

namespace FilterEx.Controllers
{
    [ApiController]
    [Route("[controller]")]
    [MySampleAsyncActionFilterAttribute("Controller")]
    public class UserController : ControllerBase
    {
        public string Get()
        {
            return "Hello World!";
        }
    }

    [ApiController]
    [Route("[controller]")]
    //[MySampleActionFilter("WeatherControllerFilter")] //Filter will be executed only when this controller is called
    [MySampleAsyncActionFilterAttribute("Controller")]
    public class WeatherForecastController : ControllerBase
    {
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
        [MySampleAsyncActionFilterAttribute("GetAction")]
        public IEnumerable<WeatherForecast> Get()
        {
            return Enumerable.Range(1, 5).Select(index => new WeatherForecast
            {
                Date = DateOnly.FromDateTime(DateTime.Now.AddDays(index)),
                TemperatureC = Random.Shared.Next(-20, 55),
                Summary = Summaries[Random.Shared.Next(Summaries.Length)]
            })
            .ToArray();
        }
    }
}
