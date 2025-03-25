using Contracts.Services.Managers.ApplicationUsers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WebShop.Extensions;

namespace WebShop.Controllers
{
    [Route("api/[controller]")]
    [Authorize]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly IApplicationUserManager applicationUserManager;

        public UsersController(IApplicationUserManager applicationUserManager)
        {
            this.applicationUserManager = applicationUserManager;
        }

        [AllowAnonymous]
        [HttpGet]
        [Route("all-usernames")]
        public async Task<IActionResult> GetAllUserNames()
        {
            var userNames = await applicationUserManager.GetAllUserNamesAsync();

            return Ok(userNames);
        }

        [HttpGet]
        [Route("get-profile-picture")]
        public async Task<IActionResult> GetProfilePicture()
        {
            var operationResult = await applicationUserManager.GetProfilePictureAsync(User.GetId());

            if (!operationResult.IsSuccessful)
            {
                return this.Error(operationResult);
            }

            byte[] imageBytes = Convert.FromBase64String(operationResult.Data);

            return File(imageBytes, "image/png");
        }

        [HttpPost]
        [Route("update-profile-picture")]
        public async Task<IActionResult> UpdateProfilePicture(IFormFile profilePicture)
        {
            if (profilePicture is null || profilePicture.Length == 0)
            {
                return BadRequest("No file uploaded.");
            }

            using var memoryStream = new MemoryStream();
            await profilePicture.CopyToAsync(memoryStream);
            var fileBytes = memoryStream.ToArray();
            string base64String = Convert.ToBase64String(fileBytes);

            var operationResult = await applicationUserManager.UpdateProfilePictureAsync(User.GetId(), base64String);

            if (!operationResult.IsSuccessful)
            {
                return this.Error(operationResult);
            }

            return Ok();
        }
    }
}
