using Microsoft.AspNetCore.Mvc;
using WeatherForecast.Domain.Models.Base;
using WeatherForecast.Domain.Services.Interfaces.Base;

namespace WeatherForecast.Controllers.Base
{
    public abstract class EntityController<T> : ExceptionHandlingController where T : Entity
    {
        private readonly IService<T> _service;

        protected EntityController(IService<T> service)
        {
            _service = service;
        }

        [HttpGet("{id:guid}")]
        public async Task<IActionResult> GetById(Guid id) =>
            await ExecuteWithExceptionHandling(async () =>
                Ok(await _service.GetById(id)));

        [HttpGet]
        public async Task<IActionResult> GetAll() =>
            await ExecuteWithExceptionHandling(async () =>
                Ok(await _service.GetAll()));

        [HttpPost("Create")]
        public async Task<IActionResult> Create(T value) =>
            await ExecuteWithExceptionHandling(async () =>
                Ok(await _service.Create(value)));

        [HttpPut]
        public async Task<IActionResult> Update(T value) =>
            await ExecuteWithExceptionHandling(async () =>
                Ok(await _service.Update(value)));

        [HttpPost("Delete/{id:guid}")]
        public async Task<IActionResult> Delete(Guid id) =>
            await ExecuteWithExceptionHandling(async () =>
                Ok(await _service.Delete(id)));
    }
}
