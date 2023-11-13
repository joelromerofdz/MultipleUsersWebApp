using Application.Services.Interfaces.Country;
using Application.Services.Interfaces.ICityService;
using Microsoft.AspNetCore.Mvc;
using Shared.Dtos.City;
using Shared.Dtos.Country;

namespace API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class CityController : ControllerBase
    {
        private readonly ICityService _cityService;
        public CityController(ICityService cityService)
        {

            _cityService = cityService;
        }

        [HttpGet(Name = "Cities")]
        public async Task<IActionResult> Get()
        {
            var cities = await _cityService.GetCitiesAsync();
            return Ok(cities);
        }

        [HttpPost]
        public async Task<ActionResult> Post([FromBody] CityRequest cityRequest)
        {
            if (cityRequest == null)
            {
                return BadRequest("No countries provided");
            }

            await _cityService.AddCityAsync(cityRequest);

            return Ok();
        }
    }
}
