using Microsoft.AspNetCore.Mvc;
using TodoList.Models;
using TodoList.SqlServer;

namespace TodoList.Controllers
{
    //[Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class TodoController : ControllerBase
    {
        private readonly DatabaseContext _context;

        public TodoController(DatabaseContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<List<TodoItem>>> GetList()
        {
            //await _context.AppUsers.FirstAsync(x => x.Id)
            return Ok();
        }
    }
}
