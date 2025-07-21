using RestApiTemplate.DTOs;

namespace RestApiTemplate.Services.Interfaces
{
    public interface IWeatherService
    {
        IEnumerable<WeatherForecastDTO> GetWeatherForecasts();
    }
}
