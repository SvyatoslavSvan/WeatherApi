using System.Linq.Expressions;
using WeatherForecast.Controllers;
using WeatherForecast.Domain.Models.Base;

namespace WeatherForecast.DAL.Interfaces.Base
{
    public interface IRepository<T> where T : Entity
    {
        public Task<T> Create(TemperatureState temperature);
        public Task<T> Delete(Guid id);
        public Task<T> Update(TemperatureState temperature);
        public Task<T> GetById(Guid id);
        public Task<T> GetAll(Expression<Func<T, bool>>? predicate = null);
    }
}
