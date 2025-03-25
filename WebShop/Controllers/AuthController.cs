using Contracts.Services.Entity.ApplicationUsers;
using Contracts.Services.Entity.Customers;
using Contracts.Services.Identity;
using Contracts.Services.JWT;
using Contracts.Services.Managers.ApplicationUsers;
using Contracts.Services.Managers.Customers;
using Database.Entities.Identity;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Models.Dto.Users.Input;
using Models.Dto.Users.Output;
using WebShop.Extensions;

namespace WebShop.Controllers
{
    [ApiController]
    [Authorize]
    [Route("api/[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly ICustomUserManager customUserManager;
        private readonly RoleManager<ApplicationRole> roleManager;
        private readonly IJwtService jwtService;
        private readonly ICustomerManager customerManager;
        private readonly IApplicationUserManager applicationUserManager;

        public AuthController(ICustomUserManager customUserManager,
            RoleManager<ApplicationRole> roleManager,
            IJwtService jWTService,
            ICustomerManager customerManager,
            IApplicationUserManager applicationUserManager)
        {
            this.customUserManager = customUserManager;
            this.roleManager = roleManager;
            this.jwtService = jWTService;
            this.customerManager = customerManager;
            this.applicationUserManager = applicationUserManager;
        }

        [AllowAnonymous]
        [HttpPost]
        [Route("login")]
        public async Task<IActionResult> Login(UserLoginDto userLoginDto)
        {
            var userId = User.GetId();

            if (userId != null)
            {
                return BadRequest("Already logged in!");
            }

            var user = await customUserManager.FindByNameAsync(userLoginDto.UserName);

            if (user == null || !(await customUserManager.CheckPasswordAsync(user, userLoginDto.Password)))
            {
                return BadRequest("Incorrect username or password!");
            }

            var token = jwtService.GenerateJwtToken(user.Id, user.UserName);

            Response.Cookies.Append("jwt", token, new CookieOptions
            {
                HttpOnly = true,
                Secure = true, // Ensure this is only true in HTTPS environments
                SameSite = SameSiteMode.None,
                Expires = DateTime.UtcNow.AddDays(14)
            });

            var userLoginOutputDto = new UserLoginOutputDto()
            {
                Id = user.Id,
                UserName = user.UserName
            };

            return Ok(userLoginOutputDto);
        }

        [AllowAnonymous]
        [HttpPost]
        [Route("register")]
        public async Task<IActionResult> Register(UserRegisterDto userRegisterDto)
        {
            var user = new ApplicationUser()
            {
                FirstName = userRegisterDto.FirstName,
                MiddleName = userRegisterDto.MiddleName,
                LastName = userRegisterDto.LastName,
                UserName = userRegisterDto.UserName,
            };

            var userNameIsTaken = await applicationUserManager.IsUserNameTakenAsync(userRegisterDto.UserName);

            if (userNameIsTaken)
            {
                return BadRequest($"Username - {userRegisterDto.UserName} is already taken!");
            }

            var operationResult = await customerManager.CreateCustomerAsync(user, userRegisterDto.Customer);

            if (!operationResult.IsSuccessful)
            {
                return this.Error(operationResult);
            }

            var createUserResult = await customUserManager.CreateAsync(user, userRegisterDto.Password);

            if (!createUserResult.Succeeded)
            {
                return BadRequest(createUserResult.Errors.Select(e => e.Description));
            }

            var addUserToRoleResult = await customUserManager.AddToRoleAsync(user, "Customer");

            if (!addUserToRoleResult.Succeeded)
            {
                return BadRequest(addUserToRoleResult.Errors.Select(e => e.Description));
            }

            var token = jwtService.GenerateJwtToken(user.Id, user.UserName);

            Response.Cookies.Append("jwt", token, new CookieOptions
            {
                HttpOnly = true,
                Secure = true, // Ensure this is only true in HTTPS environments
                SameSite = SameSiteMode.None,
                Expires = DateTime.UtcNow.AddDays(14)
            });

            var userLoginOutputDto = new UserLoginOutputDto()
            {
                Id = user.Id,
                UserName = user.UserName
            };

            return Ok(userLoginOutputDto);
        }

        [AllowAnonymous]
        [HttpPost]
        [Route("logout")]
        public async Task<IActionResult> Logout()
        {
            if (Request.Cookies["jwt"] != null)
            {
                Response.Cookies.Delete("jwt");
            }

            return Ok();
        }

        [HttpGet]
        [Route("auth-test")]
        public IActionResult AuthTest()
        {
            return Ok("You are authenticated!");
        }        
    }
}
