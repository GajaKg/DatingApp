using System.Security.Cryptography;
using System.Text;
using datingapp.api.DTOs;
using datingapp.api.Interfaces;
using datingapp.data.Data;
using datingapp.data.Entities;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace datingapp.api.Controllers
{
    public class AccountController : BaseApiController
    {
        private readonly DataContext _context;
        private readonly ITokenService _tokenService;

        public AccountController(DataContext context, ITokenService tokenService)
        {
            _context = context;
            _tokenService = tokenService;
        }

        // [HttpPost("register")] // api/account/register
        [HttpPost]
        [Route("register")]
        public async Task<ActionResult<UserDto>> Register([FromBody] RegisterDto registerDto)
        {
            if (!ModelState.IsValid) return BadRequest("Username or password is invalid");

            if (await UserExist(registerDto.UserName)) return BadRequest("Username taken!");
            // using -> allow HMACSHA512 class to be garbage collected because we do not need anymore. Has Dispose() method
            using var hmac = new HMACSHA512();

            var user = new AppUser
            {
                UserName = registerDto.UserName!,
                PasswordHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(registerDto.Password!)),
                PasswordSalt = hmac.Key
            };

            _context.Users.Add(user);
            await _context.SaveChangesAsync();

            return new UserDto
            {
                Username = user.UserName,
                Token = _tokenService.CreateToken(user)
            };
        }

        [HttpPost]
        [Route("login")]
        public async Task<IActionResult> Login([FromBody] LoginDto loginDto)
        {
            var user = await _context.Users.FirstOrDefaultAsync(u => u.UserName == loginDto.UserName);

            if (user == null) return Unauthorized("Invalid username");

            using var hmac = new HMACSHA512(user.PasswordSalt!);

            var computedHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(loginDto.Password!));

            for (int i = 0; i < computedHash.Length; i++)
            {
                if (computedHash[i] != user.PasswordHash![i]) return Unauthorized("Invalid password!");
            }

            return Ok(
                new UserDto
                {
                    Username = user.UserName,
                    Token = _tokenService.CreateToken(user)
                }
            );
        }

        private async Task<bool> UserExist(string username)
        {
            return await _context.Users.AnyAsync(x => x.UserName.ToLower() == username.ToLower());
            // return await _context.Users.AnyAsync(x => x.UserName.Equals(username, StringComparison.CurrentCultureIgnoreCase));
        }
    }

}