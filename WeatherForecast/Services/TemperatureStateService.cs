using LinqKit;
using WeatherForecast.DAL.Interfaces.Base;
using WeatherForecast.Domain.Models;
using WeatherForecast.Domain.SearchRequest;
using WeatherForecast.Domain.Services.Interfaces;
using WeatherForecast.Domain.Services.Services.Base;

namespace WeatherForecast.Domain.Services.Services
{
    public class TemperatureStateService : Service<TemperatureState>, ITemperatureStateService
    {
        public TemperatureStateService(IRepository<TemperatureState> repository) : base(repository)
        {
        }

        public async Task<IEnumerable<TemperatureState>> GetBySearchRequest(TemperatureStateSearchRequest request)
        {
            var predicate = PredicateBuilder.New<TemperatureState>();
            if (request.MinimumTemperature != null)
            {
                predicate.And(x => x.Temperature > request.MinimumTemperature);
            }
            if (request.MaximumTemperature != null)
            {
                predicate.And(x => x.Temperature < request.MaximumTemperature);
            }
            if (request.City != null)
            {
                predicate.And(x => x.CityName == request.City);
            }

            var result = await Repository.GetAll(predicate);
            return result;
        }
    }
}
