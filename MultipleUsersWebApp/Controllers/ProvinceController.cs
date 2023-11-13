using Application.Services.Interfaces.Province;
using Microsoft.AspNetCore.Mvc;
using Shared.Dtos.Province;

namespace API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ProvinceController : ControllerBase
    {
        private readonly IProvinceService _provinceService;
        public ProvinceController(IProvinceService ProvinceService)
        {

            _provinceService = ProvinceService;
        }

        [HttpGet(Name = "Provinces")]
        public async Task<IActionResult> Get()
        {
            var countries = await _provinceService.GetProvincesAsync();
            return Ok(countries);
        }

        [HttpPost]
        public async Task<ActionResult> Post([FromBody] ProvinceRequest provinceRequest)
        {
            if (provinceRequest == null)
            {
                return BadRequest("No countries provided");
            }

            await _provinceService.AddProvinceAsync(provinceRequest);

            return Ok();
        }
    }
}
