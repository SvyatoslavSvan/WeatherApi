using Microsoft.AspNetCore.Mvc;
using WeatherForecast.Domain.Interfaces;
using WeatherForecast.Domain.Models;

namespace WeatherForecast.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ForecastController : ControllerBase
    {
        private readonly IWeatherApiService _weatherApiService;

        public ForecastController(IWeatherApiService weatherApiService)
        {
            _weatherApiService = weatherApiService;
        }
        [HttpGet("{hour}")]
        public async Task<IActionResult> GetTodayTemperature(TimeOnly hour)
        {
            return Ok(await _weatherApiService.GetTodayTemperatureState(hour, new City()
            {
                Latitude = 47.875f,
                Longitude = 31.0f
            }));
        }
    }
}
