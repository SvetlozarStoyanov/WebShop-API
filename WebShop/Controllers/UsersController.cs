using Contracts.Services.Entity.ApplicationUsers;
using Contracts.Services.ProfilePictures;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WebShop.Extensions;

namespace WebShop.Controllers
{
    [Route("api/[controller]")]
    [Authorize]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly IApplicationUserService applicationUserService;
        private readonly IProfilePictureService profilePictureService;


        public UsersController(IApplicationUserService applicationUserService, IProfilePictureService profilePictureService)
        {
            this.applicationUserService = applicationUserService;
            this.profilePictureService = profilePictureService;
        }


        [AllowAnonymous]
        [HttpGet]
        [Route("all-usernames")]
        public async Task<IActionResult> GetAllUserNames()
        {
            var userNames = await applicationUserService.GetAllUserNamesAsync();

            return Ok(userNames);
        }


        [HttpGet]
        [Route("get-own-profile")]
        public async Task<IActionResult> GetOwnProfile()
        {
            var operationResult = await applicationUserService.GetUserProfileAsync(User.GetId());

            if (!operationResult.IsSuccessful)
            {
                return this.Error(operationResult);
            }

            return Ok(operationResult.Data);
        }

        [HttpGet]
        [Route("get-profile-picture")]
        public async Task<IActionResult> GetProfilePicture()
        {
            var operationResult = await profilePictureService.GetProfilePictureAsync(User.GetId());

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

            var operationResult = await profilePictureService.UpdateProfilePictureAsync(User.GetId(), base64String);

            if (!operationResult.IsSuccessful)
            {
                return this.Error(operationResult);
            }

            return Ok();
        }
    }
}
