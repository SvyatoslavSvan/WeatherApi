using Microsoft.AspNetCore.Mvc;
using WeatherForecast.Domain.Models.Base;
using WeatherForecast.Domain.Services.Interfaces.Base;

namespace WeatherForecast.Controllers.Base
{
    public abstract class EntityController<T> : ExceptionHandlingController where T : Entity
    {
        protected readonly IService<T> Service;

        protected EntityController(IService<T> service)
        {
            Service = service;
        }

        [HttpGet("{id:guid}")]
        public async Task<IActionResult> GetById(Guid id) =>
            await ExecuteWithExceptionHandling(async () =>
                Ok(await Service.GetById(id)));

        [HttpGet]
        public async Task<IActionResult> GetAll() =>
            await ExecuteWithExceptionHandling(async () =>
                Ok(await Service.GetAll()));

        [HttpPost("Create")]
        public async Task<IActionResult> Create(T value) =>
            await ExecuteWithExceptionHandling(async () =>
                Ok(await Service.Create(value)));

        [HttpPut]
        public async Task<IActionResult> Update(T value) =>
            await ExecuteWithExceptionHandling(async () =>
                Ok(await Service.Update(value)));

        [HttpPost("Delete/{id:guid}")]
        public async Task<IActionResult> Delete(Guid id) =>
            await ExecuteWithExceptionHandling(async () =>
                Ok(await Service.Delete(id)));
    }
}
