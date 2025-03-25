using Contracts.DataAccess.UnitOfWork;
using Contracts.Services.Entity.ApplicationUsers;
using Contracts.Services.Managers.ApplicationUsers;
using Contracts.Services.ProfilePictures;
using Database.Entities.Identity;
using Models.Common;

namespace Services.Managers.ApplicationUsers
{
    /// <summary>
    /// Performs <see cref="ApplicationUser"/> related operations
    /// </summary>
    public class ApplicationUserManager : IApplicationUserManager
    {
        private readonly IUnitOfWork unitOfWork;
        private IApplicationUserService applicationUserService;
        private IProfilePictureService profilePictureService;

        public ApplicationUserManager(IUnitOfWork unitOfWork, IApplicationUserService applicationUserService, IProfilePictureService profilePictureService)
        {
            this.unitOfWork = unitOfWork;
            this.applicationUserService = applicationUserService;
            this.profilePictureService = profilePictureService;
        }

        ///<inheritdoc/>
        public async Task<bool> IsUserNameTakenAsync(string userName)
        {
            return await applicationUserService.IsUserNameTakenAsync(userName);
        }

        ///<inheritdoc/>
        public async Task<IEnumerable<string>> GetAllUserNamesAsync()
        {
            return await applicationUserService.GetAllUserNamesAsync();
        }

        public async Task<OperationResult<string?>> GetProfilePictureAsync(string userId)
        {
            return await profilePictureService.GetProfilePictureAsync(userId);
        }

        public async Task<OperationResult> UpdateProfilePictureAsync(string userId, string? profilePicture)
        {
            return await profilePictureService.UpdateProfilePictureAsync(userId, profilePicture);
        }
    }
}
