using WeatherForecast.Domain.Models;

namespace WeatherForecast.Domain.Interfaces
{
    public interface IWeatherApiService
    {
        public Task<TemperatureState> GetTodayTemperatureState(TimeOnly hour, string city);

        public Task<IList<TemperatureState>> GetForecast(ForecastPeriod forecastPeriod, string cityName);
    }
}
