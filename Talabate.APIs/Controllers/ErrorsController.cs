using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Talabate.APIs.Error;

namespace Talabate.APIs.Controllers
{
    [Route("errors/{code}")]
    [ApiController]
    [ApiExplorerSettings(IgnoreApi = true)]
    public class ErrorsController : ControllerBase
    {
        public IActionResult Error(int code)
        {
            if (code == 401)
                return Unauthorized(new ApiResponse(code));
            else if (code == 404)
                return NotFound(new ApiResponse(code));
             else   
            return StatusCode(code);
        }
    }
}
