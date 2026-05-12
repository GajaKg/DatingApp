using System.Formats.Tar;
using System.Security.Cryptography;
using System.Text;
using datingapp.api.Data;
using datingapp.api.DTOs;
using datingapp.api.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace datingapp.api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AccountsController : ControllerBase
    {
        private readonly ApplicationDBContext _context;

        public AccountsController(ApplicationDBContext context)
        {
            _context = context;
        }

        [HttpPost("login")]
        public async Task<ActionResult<AppUser>> Login(LoginDto loginDto)
        {
            var user = await _context.Users.SingleOrDefaultAsync(u => u.UserName == loginDto.Username);

            if (user == null) return Unauthorized();

            using var hmac = new HMACSHA512();
            var computedHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(loginDto.Password));

            for (int i = 0; i < computedHash.Length; i++)
            {
                if (computedHash[i] != user.PasswordHash[i]) return Unauthorized("Invalid passsword");
            }

            return user;
        }

        [HttpPost("register")]
        public async Task<ActionResult<AppUser>?> Register([FromBody] RegisterDto registerDto)
        {

            // var isExist = await _context.Users.AnyAsync(x => x.UserName.Equals(registerDto.Username, StringComparison.CurrentCultureIgnoreCase));
            var isExist = await _context.Users.AnyAsync(x => x.UserName == registerDto.Username);

            if (isExist) return BadRequest("Username is taken");

            using var hmac = new HMACSHA512();
            var user = new AppUser
            {
                UserName = registerDto.Username.ToLower(),
                PasswordHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(registerDto.Password)),
                PasswordSalt = hmac.Key
            };

            _context.Users.Add(user);
            await _context.SaveChangesAsync();

            return user;
        }
    }
}