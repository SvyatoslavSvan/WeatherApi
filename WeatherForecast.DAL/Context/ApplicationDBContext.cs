using Microsoft.EntityFrameworkCore;
using WeatherForecast.Domain.Models;

namespace WeatherForecast.DAL.Context
{
    public class ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : DbContext(options)
    {
        public DbSet<TemperatureState> Temperatures { get; set; }
    }
}
