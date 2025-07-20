using Microsoft.AspNetCore.Mvc;
using RestApiTemplate.DTOs;
using RestApiTemplate.Models;
using RestApiTemplate.Services;

namespace RestApiTemplate.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class WeatherForecastController : ControllerBase
    {
        private readonly ILogger<WeatherForecastController> _logger;
        private readonly IWeatherService _weatherService;

        public WeatherForecastController(ILogger<WeatherForecastController> logger, IWeatherService weatherService)
        {
            _logger = logger;
            _weatherService = weatherService;
        }

        [HttpGet(Name = "GetWeatherForecast")]
        public IEnumerable<WeatherForecastDTO> Get()
        {
            IEnumerable<WeatherForecastDTO> weathers = _weatherService.GetWeatherForecasts();

            return weathers;
        }
    }
}
