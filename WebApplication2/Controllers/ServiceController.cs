using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApplication2.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ServiceController : ControllerBase
    {
        private static readonly string[] Summaries = new[]
        {
            "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
        };

        private readonly ILogger<ServiceController> _logger;
        private Calculator _calculator;

        public ServiceController(
            ILogger<ServiceController> logger,
            Calculator calculator
            )
        {
            _logger = logger;
            _calculator = calculator;
        }

        [HttpGet]
        public IEnumerable<WeatherForecast> Get()
        {
            var rng = new Random();
            return Enumerable.Range(1, 5).Select(index => new WeatherForecast
            {
                Date = DateTime.Now.AddDays(index),
                TemperatureC = rng.Next(-20, 55),
                Summary = Summaries[rng.Next(Summaries.Length)]
            })
            .ToArray();
        }

        [HttpGet("add/{a}/{b}")]
        public float ProcessAdd(int a, int b)
        {
            return _calculator.Add(a, b);
        }

        [HttpGet("sub/{a}/{b}")]
        public float ProcessSub(int a, int b)
        {
            return _calculator.Sub(a, b);
        }

        [HttpGet("mul/{a}/{b}")]
        public float ProcessMul(int a, int b)
        {
            return _calculator.Mul(a, b);
        }

    }
}
