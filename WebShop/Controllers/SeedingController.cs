using Contracts.Services.Seeding;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WebShop.Extensions;

namespace WebShop.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class SeedingController : ControllerBase
    {
        private readonly ISeedingService seedingService;

        public SeedingController(ISeedingService seedingService)
        {
            this.seedingService = seedingService;
        }

        [HttpPost]
        [Route("seed")]
        public async Task<IActionResult> Seed()
        {
            var operationResult = await seedingService.SeedAsync();

            if (!operationResult.IsSuccessful)
            {
                return this.Error(operationResult);
            }

            return Ok();
        }
    }
}
