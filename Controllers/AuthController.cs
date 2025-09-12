using EventXFullApi.Data;
using EventXFullApi.Models;
using EventXFullApi.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Cryptography;
using System.Text;

namespace EventXFullApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        private readonly AuthService _authService;

        public AuthController(ApplicationDbContext context, AuthService authService)
        {
            _context = context;
            _authService = authService;
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] User user)
        {
            if (await _context.Users.AnyAsync(u => u.Email == user.Email))
                return BadRequest("Email já cadastrado");

            user.PasswordHash = Convert.ToBase64String(
                SHA256.HashData(Encoding.UTF8.GetBytes(user.PasswordHash))
            );

            _context.Users.Add(user);
            await _context.SaveChangesAsync();

            return Ok(user);
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] User user)
        {
            var dbUser = await _context.Users.FirstOrDefaultAsync(u => u.Email == user.Email);
            if (dbUser == null) return Unauthorized("Usuário não encontrado");

            var hash = Convert.ToBase64String(SHA256.HashData(Encoding.UTF8.GetBytes(user.PasswordHash)));
            if (dbUser.PasswordHash != hash) return Unauthorized("Senha incorreta");

            var token = _authService.GenerateJwtToken(dbUser);
            return Ok(new { token });
        }
    }
}
