using System.Diagnostics.CodeAnalysis;

namespace WeatherForecast.Domain.Models
{
    public class City(float latitude, float longitude, string name)
    {
        public string Name { get; set; } = name;

        public float Latitude { get; set; } = latitude;

        public float Longitude { get; set; } = longitude;
    }
}
