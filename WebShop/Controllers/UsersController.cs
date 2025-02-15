using Contracts.Services.Entity.ApplicationUsers;
using Contracts.Services.Entity.Customers;
using Contracts.Services.Identity;
using Contracts.Services.JWT;
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
        [Route("logout")]
        public async Task<IActionResult> Logout()
        {
            if (Request.Cookies["jwt"] != null)
            {
                Response.Cookies.Delete("jwt");
            }
            return Ok();
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

            var userNameIsTaken = await userService.IsUserNameTakenAsync(userRegisterDto.UserName);

            if (userNameIsTaken)
            {
                return BadRequest($"Username - {userRegisterDto.UserName} is already taken!");
            }

            var operationResult = await customerService.CreateCustomerAsync(user, userRegisterDto.Customer);

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

        [HttpGet]
        [Route("auth-test")]
        public IActionResult AuthTest()
        {
            return Ok("You are authenticated!");
        }        
    }
}
