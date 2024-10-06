using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using WeatherForecast.DAL.Interfaces.Base;
using WeatherForecast.Domain.Models;
using WeatherForecast.Domain.Models.Base;

namespace WeatherForecast.DAL.Repositories.Base
{
    public class Repository<T> : IRepository<T> where T : Entity
    {
        private readonly DbContext _dbContext;

        public Repository(DbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public Task<T> Create(TemperatureState temperature)
        {
            throw new NotImplementedException();
        }

        public Task<T> Delete(Guid id)
        {
            throw new NotImplementedException();
        }

        public Task<T> Update(TemperatureState temperature)
        {
            throw new NotImplementedException();
        }

        public Task<T> GetById(Guid id)
        {
            throw new NotImplementedException();
        }

        public Task<T> GetAll(Expression<Func<T, bool>>? predicate = null)
        {
            throw new NotImplementedException();
        }
    }
}
