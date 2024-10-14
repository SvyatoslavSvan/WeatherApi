namespace WeatherForecast.DTO.Forecast
{
    public class Period(DateTime from, DateTime to)
    {
        public DateTime From { get; set; } = from;

        public DateTime To { get; set; } = to;
    }
}
