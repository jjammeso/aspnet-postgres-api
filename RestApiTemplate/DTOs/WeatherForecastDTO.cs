namespace RestApiTemplate.DTOs
{
    public class WeatherForecastDTO
    {
        public DateOnly Date { get; set; }

        public int TemperatureC { get; set; }

        public int TemperatureF => 32 + (int)(TemperatureC / 0.5556);

        public string? Summary { get; set; }

        public string? Message { get; set; }

        public WeatherForecastDTO(DateOnly date, int temperatureC, string? summary)
        {
            Date = date;
            TemperatureC = temperatureC;
            Summary = summary;
            Message = $"The temperature is {TemperatureC}";
        }
    }
}
