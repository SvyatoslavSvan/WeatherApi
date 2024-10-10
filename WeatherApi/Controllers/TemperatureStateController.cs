using Microsoft.AspNetCore.Mvc;
using WeatherForecast.Controllers.Base;
using WeatherForecast.Domain.DTO.SearchRequest;
using WeatherForecast.Domain.Models;
using WeatherForecast.Domain.Services;
using WeatherForecast.Domain.Services.Interfaces;

namespace WeatherForecast.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TemperatureStateController : EntityController<TemperatureState>
    {
        private readonly ITemperatureStateService _service;

        public TemperatureStateController(ITemperatureStateService service) : base(service)
        {
            _service = service;
        }
        [HttpGet("search")]
        public async Task<IActionResult> GetSearch([FromQuery]TemperatureStateSearchRequest request) =>
            await ExecuteWithExceptionHandling(async () =>
                Ok(await _service.GetBySearchRequest(request)));

    }
}
