
namespace WeatherForecast.DTO.Forecast
{
    public sealed class Hourly
    {
        public List<DateTime> Time { get; set; } = null!;
        // ReSharper disable once InconsistentNaming
        public List<float> Temperature_2m { get; set; } = null!;
    }
}
