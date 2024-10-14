namespace WeatherForecast.DTO.Forecast
{
    public sealed class Result
    {
        public string Name { get; set; } = null!;
        public float Latitude { get; set; }
        public float Longitude { get; set; }
    }
}
