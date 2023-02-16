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
    public class UsersController : ControllerBase
    {
        private readonly DatabaseContext _context;

        public UsersController(DatabaseContext context)
        {
            _context = context;
        }

        [HttpPost]
        public async Task<IActionResult> Register()
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var user = await _context.AppUsers.FirstOrDefaultAsync(x => x.Id == userId);

            if (userId is null)
            {
                return Unauthorized();
            }

            if (user is not null)
            {
                return Ok();
            }

            var newUser = new AppUser(userId!);

            await _context.AppUsers.AddAsync(newUser);

            if (await _context.SaveChangesAsync() > 0)
            {
                return Ok();
            }

            return BadRequest();
        }
    }
}
