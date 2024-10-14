using Microsoft.AspNetCore.Mvc;
using WeatherForecast.Controllers.Base;
using WeatherForecast.Domain.Services.Interfaces;
using WeatherForecast.DTO.WeatherForecast;

namespace WeatherForecast.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ForecastController : ExceptionHandlingController
    {
        private readonly IForecastApiService<TemperatureState> _forecastApiService;

        public ForecastController(IForecastApiService<TemperatureState> forecastApiService)
        {
            _forecastApiService = forecastApiService;
        }

        [HttpGet("TodayTemperature")]
        public async Task<IActionResult> TodayTemperature(int hour, string cityName) =>
            await ExecuteWithExceptionHandling(async () =>
                Ok(await _forecastApiService.GetTodayForecast(new TimeOnly(hour, 00), cityName)));

        [HttpGet("Forecast")]
        public async Task<IActionResult> Forecast(DateTime from, DateTime to, string cityName) =>
            await ExecuteWithExceptionHandling(async () =>
                Ok(await _forecastApiService.GetForecast(new Period(from, to), cityName)));

    }
}
