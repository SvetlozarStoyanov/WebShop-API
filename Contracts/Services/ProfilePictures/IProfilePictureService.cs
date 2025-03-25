using Database.Entities.Identity;
using Models.Common;

namespace Contracts.Services.ProfilePictures
{
    public interface IProfilePictureService
    {
        /// <summary>
        /// Gets <see cref="ApplicationUser.ProfilePicture"/> of <see cref="ApplicationUser"/> 
        /// with id: <paramref name="userId"/>
        /// </summary>
        /// <param name="userId"></param>
        /// <returns><see cref="OperationResult"/> with data - <see cref="string"/></returns>
        Task<OperationResult<string?>> GetProfilePictureAsync(string userId);

        /// <summary>
        /// Updates <see cref="ApplicationUser.ProfilePicture"/> ,of <see cref="ApplicationUser"/> with id :
        /// <paramref name="userId"/>, with <paramref name="profilePicture"/>
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="profilePicture"></param>
        /// <returns><see cref="OperationResult"/> result</returns>
        Task<OperationResult> UpdateProfilePictureAsync(string userId, string? profilePicture);

    }
}
