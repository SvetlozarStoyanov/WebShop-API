using Contracts.Services.Identity;
using Contracts.Services.JWT;
using Database.Entities.Identity;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Models.Dto.UserDtos;

namespace WebShop.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly ICustomUserManager customUserManager;
        private readonly RoleManager<ApplicationRole> roleManager;
        private readonly IJwtService jwtService;

        public AuthController(ICustomUserManager customUserManager, RoleManager<ApplicationRole> roleManager, IJwtService jWTService)
        {
            this.customUserManager = customUserManager;
            this.roleManager = roleManager;
            this.jwtService = jWTService;

        }

        [HttpPost]
        [Route("login")]
        public async Task<IActionResult> Login(UserLoginDto dto)
        {
            if (User != null)
            {
                return BadRequest("Already logged in!");
            }

            var user = await customUserManager.FindByNameAsync(dto.UserName);

            if (user == null)
            {
                return BadRequest();
            }

            var token = jwtService.GenerateJwtToken(dto.UserName);

            return Ok(token);
        }

        [HttpPost]
        [Route("register")]
        public async Task<IActionResult> Register(UserRegisterDto dto)
        {
            var user = new ApplicationUser()
            {
                FirstName = dto.FirstName,
                MiddleName = dto.MiddleName,
                LastName = dto.LastName,
                UserName = dto.UserName,
            };
            //var userNameIsTaken = await customUserManager.Users.AnyAsync(u => u.UserName == model.UserName);
            //if (userNameIsTaken)
            //{
            //    ModelState.AddModelError(string.Empty, "Username already taken!");
            //    return RedirectToAction(nameof(Register));
            //}
            var result = await customUserManager.CreateAsync(user, dto.Password);
            if (result.Succeeded)
            {
                //await signInManager.SignInAsync(user, false);
                var token = jwtService.GenerateJwtToken(user.UserName);
                return Ok(token);
            }

            return BadRequest();
        }

        [Authorize]
        [HttpGet]
        [Route("auth-test")]
        public IActionResult AuthTest()
        {
            return Ok("You are authenticated!");
        }
    }
}
