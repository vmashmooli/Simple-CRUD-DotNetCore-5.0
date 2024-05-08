using Domain.Common.Enum;
using Microsoft.AspNetCore.Mvc;

namespace Domain.Common.Helper
{
    public class Response<T> : ControllerBase
    {
        public IActionResult ResponseSending(ApiResponse<T> response)
        {
            switch ((ResponseStatusEnum)response.Status)
            {
                case ResponseStatusEnum.Success:
                    return Ok(response);
                case ResponseStatusEnum.BadRequest:
                    return BadRequest(response);
                case ResponseStatusEnum.NotFound:
                    return NotFound(response);
                case ResponseStatusEnum.Forbidden:
                    return Forbid();
                default:
                    return BadRequest(response);
            }
        }
    }
}
