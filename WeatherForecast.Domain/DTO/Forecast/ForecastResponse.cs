namespace WeatherForecast.Domain.DTO.Forecast
{
    public sealed class ForecastResponse
    {
        public Hourly Hourly { get; set; } = null!;
    }
}
