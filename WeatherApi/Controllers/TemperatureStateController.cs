using Microsoft.AspNetCore.Mvc;
using WeatherForecast.Controllers.Base;
using WeatherForecast.Domain.Models;
using WeatherForecast.Domain.Services.Interfaces.Base;

namespace WeatherForecast.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TemperatureStateController : EntityController<TemperatureState>
    {
        public TemperatureStateController(IService<TemperatureState> service) : base(service)
        {
        }

    }
}
