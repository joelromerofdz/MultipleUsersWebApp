using Application.Services.Interfaces.Users;
using Domain.Entities;
using Microsoft.AspNetCore.Mvc;
using Shared.Dtos.Users;

namespace MultipleUsersWebApp.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;
        public UserController(IUserService userService)
        {

            _userService = userService;
        }

        [HttpGet(Name = "Users")]
        public async Task<IActionResult> Get()
        {
            var users = await _userService.GetUsersAsync();
            return Ok(users);
        }

        [HttpPost("MultipleUsers")]
        public async Task<ActionResult> Post([FromBody] IEnumerable<UserRequest> users)
        {
            if (users == null || !users.Any())
            {
                return BadRequest("No users provided");
            }

            await _userService.AddMultipleUsersAsync(users);

            return Ok();
        }


        [HttpGet("UsersByCountry/{country}")]
        public async Task<IActionResult> GetUsersByCountry(string country)
        {
            var users = await _userService.GetUsersByCountryAsync(country);
            return Ok(users);
        }
    }
}