using Microsoft.EntityFrameworkCore;
using WeatherForecast.Domain.Models;

namespace WeatherForecast.DAL.Context
{
    public class ApplicationDBContext(DbContextOptions<ApplicationDBContext> options) : DbContext(options)
    {
        public DbSet<TemperatureState> Temperature { get; set; }
    }
}
