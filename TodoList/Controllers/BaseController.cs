using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace TodoList.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BaseController : ControllerBase
    {
        [HttpGet("public")]
        public IActionResult GetPublic()
        {
            return Ok(new { Message = "public message from API" });
        }

        [Authorize]
        [HttpGet("private")]
        public IActionResult GetPrivate()
        {
            return Ok(new { Message = "private message from API" });
        }
    }
}
