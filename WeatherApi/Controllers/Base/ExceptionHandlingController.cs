using Microsoft.AspNetCore.Mvc;
using WeatherForecast.Domain.Exceptions;

namespace WeatherForecast.Controllers.Base
{
    public abstract class ExceptionHandlingController : ControllerBase
    {
        protected virtual async Task<IActionResult> ExecuteWithExceptionHandling(Func<Task<IActionResult>> action)
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
