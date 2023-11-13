using Application.Services.Interfaces.Country;
using Microsoft.AspNetCore.Mvc;
using Shared.Dtos.Country;

namespace API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class CountryController : ControllerBase
    {
        private readonly ICountryService _countryService;
        public CountryController(ICountryService countryService)
        {

            _countryService = countryService;
        }

        [HttpGet(Name = "Countries")]
        public async Task<IActionResult> Get()
        {
            var countries = await _countryService.GetCountriesAsync();
            return Ok(countries);
        }

        [HttpPost]
        public async Task<ActionResult> Post([FromBody] CountryRequest countryRequest)
        {
            if (countryRequest == null)
            {
                return BadRequest("No countries provided");
            }

            await _countryService.AddCountryAsync(countryRequest);

            return Ok();
        }
    }
}
