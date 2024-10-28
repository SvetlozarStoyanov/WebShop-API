using Contracts.Services.Entity.ApplicationUsers;
using Contracts.Services.Entity.Customers;
using Contracts.Services.Identity;
using Contracts.Services.JWT;
using Database.Entities.Identity;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Models.Dto.UserDtos;
using WebShop.Extensions;

namespace WebShop.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UsersController : ControllerBase
    {
        private readonly ICustomUserManager customUserManager;
        private readonly RoleManager<ApplicationRole> roleManager;
        private readonly IJwtService jwtService;
        private readonly ICustomerService customerService;
        private readonly IApplicationUserService userService;

        public UsersController(ICustomUserManager customUserManager,
            RoleManager<ApplicationRole> roleManager,
            IJwtService jWTService,
            ICustomerService customerService,
            IApplicationUserService userService)
        {
            this.customUserManager = customUserManager;
            this.roleManager = roleManager;
            this.jwtService = jWTService;
            this.customerService = customerService;
            this.userService = userService;
        }

        [HttpPost]
        [Route("login")]
        public async Task<IActionResult> Login(UserLoginDto dto)
        {
            var userId = User.GetId();

            if (userId != null)
            {
                return BadRequest("Already logged in!");
            }

            var user = await customUserManager.FindByNameAsync(dto.UserName);

            if (user == null || !(await customUserManager.CheckPasswordAsync(user, dto.Password)))
            {
                return BadRequest("Incorrect username or password!");
            }

            var token = jwtService.GenerateJwtToken(user.Id, user.UserName);

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

            var userNameIsTaken = await userService.IsUserNameTakenAsync(dto.UserName);

            if (userNameIsTaken)
            {
                return BadRequest($"Username - {dto.UserName} is already taken!");
            }

            await customerService.CreateCustomerAsync(user);

            var result = await customUserManager.CreateAsync(user, dto.Password);
            if (result.Succeeded)
            {
                var token = jwtService.GenerateJwtToken(user.Id, user.UserName);
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
