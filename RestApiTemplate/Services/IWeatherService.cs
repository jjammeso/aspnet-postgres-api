using RestApiTemplate.DTOs;
using RestApiTemplate.Models;

namespace RestApiTemplate.Services
{
    public interface IWeatherService
    {
        IEnumerable<WeatherForecast> GetWeatherForecasts();
    }
}
