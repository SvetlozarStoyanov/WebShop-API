using Contracts.Services.Entity.Countries;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace WebShop.Controllers
{
    [ApiController]
    [Authorize]
    [Route("api/[controller]")]
    public class CountriesController : ControllerBase
    {
        private readonly ICountryService countryService;

        public CountriesController(ICountryService countryService)
        {
            this.countryService = countryService;
        }

        [HttpGet]
        [AllowAnonymous]
        [Route("all-ddm")]
        public async Task<IActionResult> GetAllForDDM()
        {
            var countries = await countryService.GetCountriesDropdownAsync();

            return Ok(countries);
        }
    }
}
