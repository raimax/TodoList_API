using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;
using TodoList.Models;
using TodoList.SqlServer;

namespace TodoList.Controllers
{
    [Authorize]
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
        [ProducesResponseType(typeof(List<TodoItemRespose>), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetList()
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            if (userId is null)
            {
                return Unauthorized();
            }

            var items = await _context.TodoItems
                .Where(x => x.AppUserId == userId)
                .ToListAsync();

            return Ok(items);
        }

        [HttpGet("{id}")]
        [ProducesResponseType(typeof(TodoItemRespose), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Get([FromRoute] Guid id)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            if (userId is null)
            {
                return Unauthorized();
            }

            var item = await _context.TodoItems
                .FirstOrDefaultAsync(x => x.Id == id && x.AppUserId == userId);

            if (item is null)
            {
                return NotFound();
            }

            return Ok(item);
        }

        [HttpPost]
        [ProducesResponseType(typeof(TodoItemRespose), StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Create(ItemRequest input)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            if (userId is null)
            {
                return Unauthorized();
            }

            var item = new TodoItem
            {
                Content = input.Content,
                AppUserId = userId,
                Created = DateTimeOffset.UtcNow
            };

            await _context.TodoItems.AddAsync(item);

            if (await _context.SaveChangesAsync() > 0)
            {
                return CreatedAtAction("Get", new { item.Id }, item);
            }

            return BadRequest();
        }

        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Remove([FromRoute] Guid id)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            if (userId is null)
            {
                return Unauthorized();

            }
            var item = await _context.TodoItems
                .FirstOrDefaultAsync(x => x.Id == id && x.AppUserId == userId);

            if (item is null)
            {
                return NotFound();
            }

            _context.TodoItems.Remove(item);

            if (await _context.SaveChangesAsync() > 0)
            {
                return NoContent();
            }

            return BadRequest();
        }
    }
}
