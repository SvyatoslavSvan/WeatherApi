namespace WeatherForecast.Domain.Models
{
    public class TemperatureState(DateTime time, float temperature)
    {
        public DateTime DateTime { get; set; } = time;

        public float Temperature { get; set; } = temperature;
    }
}
