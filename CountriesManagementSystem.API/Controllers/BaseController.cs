using Microsoft.AspNetCore.Mvc;
using CountriesManagementSystem.Application.Common.Models;

namespace CountriesManagementSystem.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status500InternalServerError)]
    public abstract class BaseController : ControllerBase
    {
        // For generic DomainResult<T>
        protected IActionResult ReturnResponse<T>(DomainResult<T> result)
        {
            if (result.IsSuccess)
            {
                return Ok(new ApiResponse<T>(result.Value, result.Messages.ToList()));
            }

            return BadRequest(new ApiResponse<T>(result.Messages.ToList()));
        }

        // For non-generic DomainResult
        protected IActionResult ReturnResponse(DomainResult result)
        {
            if (result.IsSuccess)
            {
                return Ok(ApiResponse.Success(result.Messages.ToList()));
            }

            return BadRequest(ApiResponse.Failure(result.Messages.ToList()));
        }
    }
}
