using Database.Entities.Identity;
using Microsoft.AspNetCore.Identity;
using System.Security.Claims;

namespace Contracts.Services.Identity
{
    /// <summary>
    /// Created to encapsulate the default <see cref="UserManager{TUser}"/> 
    /// in order to restrict methods which should not be used in the given application
    /// </summary>
    public interface ICustomUserManager
    {
        /// <summary>
        /// Checks if <paramref name="password"/> matches the <paramref name="applicationUser"/>'s password
        /// </summary>
        /// <param name="applicationUser"></param>
        /// <param name="password"></param>
        /// <returns><see cref="bool"/> result</returns>
        Task<bool> CheckPasswordAsync(ApplicationUser applicationUser, string password);

        /// <summary>
        /// Gets <see cref="ApplicationUser"/> with id - <paramref name="id"/>
        /// </summary>
        /// <param name="id"></param>
        /// <returns><see cref="ApplicationUser"/> applicationUser</returns>
        Task<ApplicationUser> FindByIdAsync(string id);

        /// <summary>
        /// Gets <see cref="ApplicationUser"/> with name - <paramref name="userName"/>
        /// </summary>
        /// <param name="userName"></param>
        /// <returns><see cref="ApplicationUser"/> applicationUser</returns>
        Task<ApplicationUser> FindByNameAsync(string userName);

        /// <summary>
        /// Gets <see cref="ApplicationUser"/> from <paramref name="claimsPrincipal"/>
        /// </summary>
        /// <param name="claimsPrincipal"></param>
        /// <returns><see cref="ApplicationUser"/> applicationUser</returns>
        Task<ApplicationUser> GetUserAsync(ClaimsPrincipal claimsPrincipal);

        /// <summary>
        /// Creates <see cref="ApplicationUser"/> with <paramref name="password"/> 
        /// </summary>
        /// <param name="applicationUser"></param>
        /// <param name="password"></param>
        /// <returns><see cref="IdentityResult"/> result</returns>
        Task<IdentityResult> CreateAsync(ApplicationUser applicationUser, string password);

        /// <summary>
        /// Adds <paramref name="applicationUser"/> to role  - <paramref name="roleName"/>
        /// </summary>
        /// <param name="applicationUser"></param>
        /// <param name="roleName"></param>
        /// <returns><see cref="IdentityResult"/></returns>
        Task<IdentityResult> AddToRoleAsync(ApplicationUser applicationUser, string roleName);
    }
}
