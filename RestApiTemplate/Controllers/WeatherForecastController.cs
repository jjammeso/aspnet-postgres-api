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
            IEnumerable<WeatherForecast> weathers = _weatherService.GetWeatherForecasts();

            IEnumerable<WeatherForecastDTO> weatherDtos = from weather in weathers select new WeatherForecastDTO(weather.Date, weather.TemperatureC, weather.Summary);

            return weatherDtos;
        }
    }
}
