using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using grpcservice;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class WeatherForecastController : ControllerBase
    {
        private static readonly string[] Summaries = new[]
        {
            "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
        };

        private readonly ILogger<WeatherForecastController> _logger;
        private readonly grpcservice.Greeter.GreeterClient greeterClient;

        public WeatherForecastController(ILogger<WeatherForecastController> logger, Greeter.GreeterClient greeterClient)
        {
            _logger = logger;
            this.greeterClient = greeterClient;
        }

        [HttpGet]
        public async Task<IEnumerable<WeatherForecast>> Get()
        {
            var rng = new Random();
            var reuquest = new HelloRequest { Name = "mohit" };
            var message = await this.greeterClient.SayHelloAsync(reuquest);
            return Enumerable.Range(1, 5).Select(index => new WeatherForecast
            {
                Date = DateTime.Now.AddDays(index),
                TemperatureC = rng.Next(-20, 55),
                Summary = Summaries[rng.Next(Summaries.Length)] + message
            })
            .ToArray();
        }
    }
}
