using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using WeatherForecast.DAL.Context;
using WeatherForecast.DAL.Interfaces.Base;
using WeatherForecast.Domain.Models.Base;

namespace WeatherForecast.DAL.Repositories.Base
{
    public class Repository<T> : IRepository<T> where T : Entity
    {
        protected readonly ApplicationDbContext Context;

        public Repository(ApplicationDbContext context)
        {
            Context = context;
        }


        public async Task<T> Create(T value)
        {
            await Context.Set<T>().AddAsync(value);
            await Context.SaveChangesAsync();
            return value;
        }

        public async Task<Guid> Delete(Guid id)
        {
            await Context.Set<T>().Where(x => x.Id == id).ExecuteDeleteAsync();
            return id;
        }

        public async Task<T> Update(T value)
        {
            Context.Set<T>().Update(value);
            await Context.SaveChangesAsync();
            return value;
        }

        public async Task<T> GetById(Guid id) => await Context.Set<T>().FirstAsync(x => x.Id == id);

        public async Task<IList<T>> GetAll(Expression<Func<T, bool>>? predicate = null) => predicate == null
            ? await Context.Set<T>().ToListAsync()
            : await Context.Set<T>().Where(predicate).ToListAsync();

    }
}
