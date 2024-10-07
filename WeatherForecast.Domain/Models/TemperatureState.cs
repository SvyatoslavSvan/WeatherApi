using System.Text.Json.Serialization;
using WeatherForecast.Domain.Models.Base;

namespace WeatherForecast.Domain.Models
{
    public class TemperatureState : Entity
    {
        [JsonConstructor]
        public TemperatureState(DateTime time, float temperature, Guid id = default) : base(id)
        {
            Time = time;
            Temperature = temperature;
        }

        public DateTime Time { get; init; }

        public float Temperature { get; init; }
    }
}
