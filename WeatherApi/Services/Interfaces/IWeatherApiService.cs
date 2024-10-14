using WeatherForecast.DTO.Forecast;

namespace WeatherForecast.Domain.Services.Interfaces
{
    public interface IWeatherApiService
    {
        public Task<Hourly> GetTodayTemperatureState(TimeOnly hour, string city);

        public Task<Hourly> GetForecast(Period forecastPeriod, string cityName);
    }
}
