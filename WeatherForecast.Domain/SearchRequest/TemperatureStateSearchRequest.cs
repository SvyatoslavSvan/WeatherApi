namespace WeatherForecast.Domain.SearchRequest
{
    public class TemperatureStateSearchRequest
    {
        public float? MinimumTemperature { get; set; }

        public float? MaximumTemperature { get; set; }

        public string? City { get; set; }
    }
}
