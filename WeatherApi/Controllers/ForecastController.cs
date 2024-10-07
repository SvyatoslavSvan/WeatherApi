using Microsoft.AspNetCore.Mvc;
using WeatherForecast.Controllers.Base;
using WeatherForecast.Domain.Exceptions;
using WeatherForecast.Domain.Services.Interfaces;
using WeatherForecast.Domain.Models;

namespace WeatherForecast.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ForecastController : ExceptionHandlingController
    {
        private readonly IWeatherApiService _weatherApiService;

        public ForecastController(IWeatherApiService weatherApiService)
        {
            _weatherApiService = weatherApiService;
        }

        [HttpGet("TodayTemperature")]
        public async Task<IActionResult> TodayTemperature(int hour, string cityName) =>
            await ExecuteWithExceptionHandling(async () =>
                Ok(await _weatherApiService.GetTodayTemperatureState(new TimeOnly(hour, 00), cityName)));

        [HttpGet("Forecast")]
        public async Task<IActionResult> Forecast(DateTime from, DateTime to, string cityName) =>
            await ExecuteWithExceptionHandling(async () =>
                Ok(await _weatherApiService.GetForecast(new ForecastPeriod(from, to), cityName)));

    }
}
