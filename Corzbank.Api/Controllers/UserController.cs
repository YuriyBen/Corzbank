using Microsoft.AspNetCore.Mvc;

namespace Corzbank.Api.Controllers
{
    [Route("user")]
    public class UserController: ControllerBase
    {

        [HttpGet]
        public IActionResult Test()
        {
            return Ok(new { success = true });
        }
    }
}
