namespace WeatherForecast.Domain.DTO.SearchRequest
{
    public class TemperatureStateSearchRequest
    {
        public float? MinimumTemperature { get; set; }

        public float? MaximumTemperature { get; set; }

        public string? City { get; set; }
    }
}
