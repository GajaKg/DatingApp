using datingapp.data.Data;
using datingapp.data.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace datingapp.api.Controllers
{
    public class UsersController : BaseApiController
    {
        private readonly DataContext _context;

        public UsersController(DataContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<AppUser>>> GetUsers()
        {
            var users = await _context.Users.ToListAsync();

            if (users == null) return BadRequest();

            return users;
        }

        // [HttpGet]
        // [Route("{id:int}")]
        // public ActionResult<AppUser> GetUser([FromRoute] int id)
        [Authorize]
        [HttpGet("{id}")]
        public async Task<IActionResult> GetUserById(int id)
        {
            var user = await _context.Users.FindAsync(id);

            if (user == null) return NotFound("User not found");
            return Ok(user);
        }
    }
}