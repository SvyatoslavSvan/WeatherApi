using WeatherForecast.Domain.Models.Base;

namespace WeatherForecast.Domain.Services.Interfaces.Base
{
    public interface IService<T> where T : Entity
    {
        public Task<T> Create(T value);
        public Task<Guid> Delete(Guid id);
        public Task<T> Update(T value);
        public Task<T> GetById(Guid id);
        public Task<IList<T>> GetAll();
    }
}
