using WeatherForecast.Domain.DTO.SearchRequest;
using WeatherForecast.Domain.Models;
using WeatherForecast.Domain.Services.Interfaces.Base;

namespace WeatherForecast.Domain.Services.Interfaces
{
    public interface ITemperatureStateService : IService<TemperatureState>
    {
        public Task<IEnumerable<TemperatureState>> GetBySearchRequest(TemperatureStateSearchRequest request);
    }
}
