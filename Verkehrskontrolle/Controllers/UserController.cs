using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Verkehrskontrolle.Interfaces;
using Verkehrskontrolle.Models;
using Verkehrskontrolle.Services;

namespace Verkehrskontrolle.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class UserController : ControllerBase
    {
        private readonly IUserManager _userManager;

        public UserController(IUserManager userManager)
        {
            _userManager = userManager;
        }


        [HttpGet]
        [AllowAnonymous]
        public async Task<ActionResult<List<User>>> GetAll()
        {
            return Ok(await _userManager.GetAllUserAsync());
        }
        

        [HttpPost("login")]
        [AllowAnonymous]
        public async Task<ActionResult<string>> Authenticate(LoginCredentials credentials)
        {

            var token = await _userManager.Login(credentials);
            return token switch
            {
                UserManager.INVALIDUSER => NotFound("User not found."),
                UserManager.INVALIDPASSWORD => BadRequest("Password invalid."),
                _ => Ok(token)
            };

        }

        [HttpPost]
        public async Task<ActionResult<User>> CreateUser(User user)
        {
            var created = await _userManager.CreateUserAsync(user);
            if (created == null)
                return BadRequest("User already exists.");

            return Created(String.Empty, created);
        }

        [HttpPut("{email}")]
        public async Task<ActionResult<User>> UpdateUserPassword(string email, string password)
        {
            var updatet = await _userManager.UpdateUserAsync(new User() { Email = email, Password = password});
            if (updatet == null)
                return NotFound("User not found.");

            return Ok(updatet);
        }

        [HttpDelete("{email}")]
        [Authorize]
        public async Task<ActionResult<User>> DeleteUser(string email)
        {
            var result = await _userManager.DeleteUserAsync(email);
            
            if(!result)
                return NotFound("User not found.");

            return NoContent();
        }
    }
}
