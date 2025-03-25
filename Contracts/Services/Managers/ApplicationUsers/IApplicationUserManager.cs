using Database.Entities.Identity;
using Models.Common;

namespace Contracts.Services.Managers.ApplicationUsers
{
    /// <summary>
    /// Performs <see cref="ApplicationUser"/> related operations
    /// </summary>
    public interface IApplicationUserManager
    {
        /// <summary>
        /// Returns <see cref="true"/> if <paramref name="userName"/> is taken by another <see cref="ApplicationUser"/>
        /// </summary>
        /// <param name="userName"></param>
        /// <returns><see cref="bool"/> result</returns>
        Task<bool> IsUserNameTakenAsync(string userName);

        /// <summary>
        /// Returns all taken usernames
        /// </summary>
        /// <returns><see cref="IEnumerable"/> of type <see cref="string"/></returns>
        Task<IEnumerable<string>> GetAllUserNamesAsync();

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
