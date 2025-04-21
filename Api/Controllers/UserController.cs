using Application.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Syfora_Test.Models;
using System.Net;

namespace Syfora_Test.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;

        public UserController(IUserService dataProvider)
        {
            _userService = dataProvider;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllUsers()
        {
            var users = await _userService.GetAllUsersAsync();
            return Ok(users);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetUser(Guid id)
        {
            var user = await _userService.GetUserAsync(id);
            if (user == null) return NotFound();
            return Ok(user);
        }

        [HttpPost]
        public async Task<IActionResult> CreateUser([FromBody] UserDto userModel)
        {
            try
            {                
                if (await _userService.IsLoginExist(userModel.Login))
                {
                    return BadRequest("User login is not unique");
                }
                var user = await _userService.AddUserAsync(userModel);
                return Ok(user);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateUser(Guid id, [FromBody] UserDto user)
        {
            try
            {
                if ((await _userService.GetUserAsync(id)) == null)
                {
                    return NotFound();
                }                
                await _userService.UpdateUserAsync(user);
                return NoContent();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteUser(Guid id)
        {
            try
            {
                if ((await _userService.GetUserAsync(id)) == null)
                {
                    return NotFound();
                }
                await _userService.DeleteUserAsync(id);
                return NoContent();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
