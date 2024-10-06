using Microsoft.AspNetCore.Mvc;
using WeatherForecast.Domain.Exceptions;
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

        [HttpGet("TodayTemperature")]
        public async Task<IActionResult> TodayTemperature(int hour, string cityName)
        {
            return await ExecuteWithExceptionHandling(async () =>
                Ok(await _weatherApiService.GetTodayTemperatureState(new TimeOnly(hour, 00), cityName)));
        }

        [HttpGet("Forecast")]
        public async Task<IActionResult> Forecast(DateTime from, DateTime to, string cityName)
        {
            return await ExecuteWithExceptionHandling(async () =>
                Ok(await _weatherApiService.GetForecast(new ForecastPeriod(from, to), cityName)));
        }

        private async Task<IActionResult> ExecuteWithExceptionHandling(Func<Task<IActionResult>> action)
        {
            try
            {
                return await action();
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (HttpRequestException e)
            {
                return StatusCode(500);
            }
            catch (NotFoundException)
            {
                return NotFound();
            }
        }
    }
}
