using Contracts.Services.Entity.ApplicationUsers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace WebShop.Controllers
{
    [Route("api/[controller]")]
    [Authorize]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly IApplicationUserService applicationUserService;

        public UsersController(IApplicationUserService applicationUserService)
        {
            this.applicationUserService = applicationUserService;
        }

        [AllowAnonymous]
        [HttpGet]
        [Route("all-usernames")]
        public async Task<IActionResult> GetAllUserNames()
        {
            var userNames = await applicationUserService.GetAllUserNamesAsync();

            return Ok(userNames);
        }
    }
}
