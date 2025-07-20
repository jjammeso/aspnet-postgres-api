using RestApiTemplate.DTOs;

namespace RestApiTemplate.Services
{
    public interface IWeatherService
    {
        IEnumerable<WeatherForecastDTO> GetWeatherForecasts();
    }
}
