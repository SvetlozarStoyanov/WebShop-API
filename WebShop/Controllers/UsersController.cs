using Contracts.Services.Entity.ApplicationUsers;
using Contracts.Services.Entity.Customers;
using Contracts.Services.Identity;
using Contracts.Services.JWT;
using Database.Entities.Identity;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Models.Dto.User;
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

            return Ok(token);
        }

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
            return Ok(token);
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
