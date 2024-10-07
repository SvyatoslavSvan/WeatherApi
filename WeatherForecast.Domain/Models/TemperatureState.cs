using System.Text.Json.Serialization;
using WeatherForecast.Domain.Models.Base;

namespace WeatherForecast.Domain.Models
{
    public class TemperatureState : Entity
    {
        [JsonConstructor]
        public TemperatureState(DateTime time, float temperature,string cityName, Guid id = default) : base(id)
        {
            Time = time;
            Temperature = temperature;
            CityName = string.IsNullOrWhiteSpace(cityName) ? throw new ArgumentNullException(nameof(cityName), "CityName cannot be null") : cityName;
        }

        public DateTime Time { get; init; }

        public float Temperature { get; init; }

        public string CityName { get; init; }
    }
}
