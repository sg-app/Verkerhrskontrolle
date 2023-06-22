
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Verkehrskontrolle.Data;
using Verkehrskontrolle.Extensions;
using Verkehrskontrolle.Middleware;
using Verkehrskontrolle.Models;

namespace Verkehrskontrolle.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly AuthDbContext _context;
        private readonly JwtSettings _jwtSettings;

        public UserController(AuthDbContext context, IOptions<JwtSettings> jwtSettings)
        {
            _context = context;
            _jwtSettings = jwtSettings.Value;
        }


        [HttpGet]
        [Authorize]
        public async Task<ActionResult<List<User>>> GetAll()
        {
            return Ok(await _context.Users.ToListAsync());
        }
        

        [HttpPost("authenticate")]
        public async Task<ActionResult<AuthenticateResponse?>> Authenticate(string email, string password)
        {

            var user = await _context.Users.FindAsync(email);

            if (user == null)
                return NotFound("User not found.");
            
            if (password.CreateMD5() != user.Password)
                return BadRequest("Password not correct.");
            
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_jwtSettings.TokenKey);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new[] { new Claim("email", user.Email) }),
                Expires = DateTime.Today.AddDays(1),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);
            
            return Ok(new AuthenticateResponse() { User = user, Token = tokenHandler.WriteToken(token) }); 
        }

        [HttpPost]
        public async Task<ActionResult<User>> CreateUser(User user)
        {
            user.Password = user.Password.CreateMD5();
            //user.Token = Guid.NewGuid();

            await _context.Users.AddAsync(user);
            await _context.SaveChangesAsync();

            return Created(String.Empty, user);
        }

        [HttpDelete("{email}")]
        [Authorize]
        public async Task<ActionResult<User>> CreateUser(string email)
        {
            var toDelete = await _context.Users.FindAsync(email);
            if(toDelete == null)
                return BadRequest("User not found.");

            _context.Remove(toDelete);
            await _context.SaveChangesAsync();

            return NoContent();
        }
    }
}
