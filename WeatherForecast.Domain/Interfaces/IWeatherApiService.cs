using WeatherForecast.Domain.Models;

namespace WeatherForecast.Domain.Interfaces
{
    public interface IWeatherApiService
    {
        public Task<TemperatureState> GetTodayTemperatureState(TimeOnly hour, City city);

        public TemperatureState GetForecast(Period period, City city);
    }
}
