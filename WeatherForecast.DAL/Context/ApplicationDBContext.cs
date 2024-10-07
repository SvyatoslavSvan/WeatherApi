using Microsoft.EntityFrameworkCore;
using WeatherForecast.Domain.Models;

namespace WeatherForecast.DAL.Context
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        }

        public DbSet<TemperatureState> Temperatures { get; set; }
    }
}
