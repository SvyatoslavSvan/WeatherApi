namespace WeatherForecast.Domain.DTO.SearchCity
{
    public sealed class Result
    {
        public long Id { get; set; }
        public string Name { get; set; } = null!;
        public float Latitude { get; set; }
        public float Longitude { get; set; }
        public string Country { get; set; } = null!;
    }
}
