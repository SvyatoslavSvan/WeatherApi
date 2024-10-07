using WeatherForecast.DAL.Interfaces.Base;
using WeatherForecast.Domain.Models.Base;
using WeatherForecast.Domain.Services.Interfaces.Base;

namespace WeatherForecast.Domain.Services.Services.Base
{
    public class Service<T> : IService<T> where T : Entity
    {
        protected readonly IRepository<T> Repository;

        public Service(IRepository<T> repository)
        {
            Repository = repository;
        }

        public async Task<T> Create(T value) => await Repository.Create(value);

        public async Task<Guid> Delete(Guid id) => await Repository.Delete(id);

        public async Task<T> Update(T value) => await Repository.Update(value);

        public async Task<T> GetById(Guid id) => await Repository.GetById(id);

        public async Task<IList<T>> GetAll() => await Repository.GetAll();
    }
}
