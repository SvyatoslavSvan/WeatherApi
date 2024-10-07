using System.Linq.Expressions;
using WeatherForecast.Domain.Models.Base;

namespace WeatherForecast.DAL.Interfaces.Base
{
    public interface IRepository<T> where T : Entity
    {
        public Task<T> Create(T value);
        public Task<Guid> Delete(Guid id);
        public Task<T> Update(T value);
        public Task<T> GetById(Guid id);
        public Task<IList<T>> GetAll(Expression<Func<T, bool>>? predicate = null);
    }
}
