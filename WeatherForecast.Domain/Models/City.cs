namespace WeatherForecast.Domain.Models
{
    public sealed class City
    {
        private const float MinLatitude = -90;
        private const float MaxLatitude = 90;
        private const float MinLongitude = -180;
        private const float MaxLongitude = 180;

        private readonly float _latitude;
        private readonly float _longitude;

        public City(float latitude, float longitude, string name)
        {
            Name = !string.IsNullOrWhiteSpace(name)
                ? name
                : throw new ArgumentNullException(nameof(name), "City name cannot be null, empty, or whitespace.");
            Latitude = latitude;
            Longitude = longitude;
        }

        public string Name { get; init; }

        public float Latitude   
        {
            get => _latitude;
            init
            {
                if (value is >= MinLatitude and <= MaxLatitude)
                {
                    _latitude = value;
                }
                else
                {
                    throw new ArgumentException($"Latitude value must be between {MinLatitude} and {MaxLatitude} degrees.", nameof(Latitude));
                }
            }
        }

        public float Longitude  
        {
            get => _longitude;
            init
            {
                if (value is >= MinLongitude and <= MaxLongitude)
                {
                    _longitude = value;
                }
                else
                {
                    throw new ArgumentException($"Longitude value must be between {MinLongitude} and {MaxLongitude} degrees.", nameof(Longitude));
                }
            }
        }
    }
}
