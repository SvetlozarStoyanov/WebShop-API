using Contracts.Services.Identity;
using Database.Entities.Identity;
using Microsoft.AspNetCore.Identity;
using System.Security.Claims;

namespace Services.Identity.UserManager
{
    /// <summary>
    /// Created to encapsulate the default <see cref="UserManager{TUser}"/> 
    /// in order to restrict methods which should not be used in the given application
    /// </summary>
    public class CustomUserManager : ICustomUserManager
    {
        private readonly UserManager<ApplicationUser> userManager;

        public CustomUserManager(UserManager<ApplicationUser> userManager)
        {
            this.userManager = userManager;
        }

        ///<inheritdoc/>
        public async Task<ApplicationUser> FindByIdAsync(string id)
        {
            return await userManager.FindByIdAsync(id);
        }

        ///<inheritdoc/>
        public async Task<ApplicationUser> FindByNameAsync(string name)
        {
            return await userManager.FindByNameAsync(name);
        }

        ///<inheritdoc/>
        public async Task<ApplicationUser> GetUserAsync(ClaimsPrincipal claimsPrincipal)
        {
            return await userManager.GetUserAsync(claimsPrincipal);
        }

        ///<inheritdoc/>
        public async Task<bool> CheckPasswordAsync(ApplicationUser user, string password)
        {
            return await userManager.CheckPasswordAsync(user, password);
        }

        ///<inheritdoc/>
        public async Task<IdentityResult> CreateAsync(ApplicationUser user, string password)
        {
           return await userManager.CreateAsync(user, password);
        }

        ///<inheritdoc/>
        public async Task<IdentityResult> AddToRoleAsync(ApplicationUser user, string roleName) 
        {
            return await userManager.AddToRoleAsync(user, roleName);
        }
    }
}
