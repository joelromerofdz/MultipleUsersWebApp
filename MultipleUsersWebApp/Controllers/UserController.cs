using Application.Services.Interfaces.Users;
using Microsoft.AspNetCore.Mvc;
using Shared.Dtos.Users;
using Microsoft.Extensions.Caching.Memory;

namespace MultipleUsersWebApp.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;
        private readonly IMemoryCache _memoryCache;
        public UserController(
            IUserService userService,
            IMemoryCache memoryCache)
        {
            _userService = userService;
            _memoryCache = memoryCache;
        }

        [HttpGet(Name = "Users")]
        public async Task<IActionResult> Get()
        {
            string cacheKey = "usersData";

            if (_memoryCache.TryGetValue(cacheKey, out IEnumerable<UserResponse> usersData))
            {
                return Ok(usersData);
            }

            usersData = await _userService.GetUsersAsync();

            var cacheEntryOptions = new MemoryCacheEntryOptions
            {
                AbsoluteExpirationRelativeToNow = TimeSpan.FromMinutes(2)
            };

            _memoryCache.Set(cacheKey, usersData, cacheEntryOptions);
            return Ok(usersData);
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
            string cacheKey = "userData";

            if (_memoryCache.TryGetValue(cacheKey, out IEnumerable<UserResponse> usersData))
            {
                return Ok(usersData);
            }

            usersData = await _userService.GetUsersByCountryAsync(country);

            var cacheEntryOptions = new MemoryCacheEntryOptions
            {
                AbsoluteExpirationRelativeToNow = TimeSpan.FromMinutes(2)
            };

            _memoryCache.Set(cacheKey, usersData, cacheEntryOptions);
            return Ok(usersData);
        }
    }
}