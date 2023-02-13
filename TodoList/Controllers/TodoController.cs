using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
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
        [ProducesResponseType(typeof(List<TodoItemRespose>), 200)]
        public async Task<IActionResult> GetList()
        {
            var items = await _context.TodoItems.ToListAsync();

            return Ok(items);
        }

        [HttpGet]
        [ProducesResponseType(typeof(TodoItemRespose), 200)]
        [ProducesResponseType(404)]
        public async Task<IActionResult> Get([FromQuery] Guid id)
        {
            var item = await _context.TodoItems.FirstOrDefaultAsync(x => x.Id == id);

            if (item is null)
            {
                return NotFound();
            }

            return Ok(item);
        }

        [HttpPost]
        [ProducesResponseType(typeof(TodoItemRespose), 201)]
        [ProducesResponseType(400)]
        public async Task<IActionResult> Create(ItemRequest input)
        {
            var item = new TodoItem
            {
                Content = input.Content,
            };

            await _context.TodoItems.AddAsync(item);

            if (await _context.SaveChangesAsync() > 0)
            {
                return CreatedAtAction("Get", new { item.Id }, item);
            }

            return BadRequest();
        }

        [HttpDelete]
        [ProducesResponseType(204)]
        [ProducesResponseType(404)]
        public async Task<IActionResult> Remove([FromQuery] Guid id)
        {
            var item = await _context.TodoItems.FirstOrDefaultAsync(x => x.Id == id);

            if (item is null)
            {
                return NotFound();
            }

            _context.TodoItems.Remove(item);

            return NoContent();
        }
    }
}
