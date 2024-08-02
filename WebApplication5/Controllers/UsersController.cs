using Microsoft.AspNetCore.Mvc;
using WebApplication5.Models;
using WebApplication5.Services;
using WebApplication5.Helpers;
using Microsoft.AspNetCore.Authorization;

namespace WebApplication5.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly IUserService _userService;

        public UsersController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<User>>> GetUsers()
        {
            return await _userService.GetAllAsync();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<User>> GetUser(Guid id)
        {
            var user = await _userService.GetByIdAsync(id);

            if (user == null)
            {
                return NotFound();
            }

            return user;
        }

        [HttpPut("{id}")]
        [Authorize]
        public async Task<IActionResult> PutUser(Guid id, User user)
        {
            if (id != user.Id)
            {
                return BadRequest();
            }

            if (!_userService.UserExists(id))
            {
                return NotFound();
            }

            await _userService.Update(user);

            return NoContent();
        }

        [HttpPost]
        [Authorize]
        public async Task<ActionResult<User>> PostUser(User user)
        {
            await _userService.Add(user);

            return CreatedAtAction("GetUser", new { id = user.Id }, user);
        }

        [HttpDelete("{id}")]
        [Authorize]
        public async Task<IActionResult> DeleteUser(Guid id)
        {
            await _userService.Delete(id);

            return NoContent();
        }

        [HttpPost]
        [Route("login")]
        public async Task<ActionResult<LoginModel>> Login(string login, string password)
        {
            var user = _userService.GetByLogin(login, password);
            if (user == null)
            {
                return Unauthorized();
            }

            if (user.Role == "Administrator")
            {
                return new LoginModel { Token = AuthHelpers.GenerateJWTToken(login, password) };
            }
            
            return Ok();
        }
    }
}
