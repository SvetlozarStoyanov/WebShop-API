using Contracts.Services.Identity;
using Database.Entities.Identity;
using Microsoft.AspNetCore.Identity;
using System.Security.Claims;

namespace Services.Identity.UserManager
{
    public class CustomUserManager : ICustomUserManager
    {
        private readonly UserManager<ApplicationUser> userManager;

        public CustomUserManager(UserManager<ApplicationUser> userManager)
        {
            this.userManager = userManager;
        }

        public async Task<ApplicationUser> FindByIdAsync(string id)
        {
            return await userManager.FindByIdAsync(id);
        }

        public async Task<ApplicationUser> FindByNameAsync(string name)
        {
            return await userManager.FindByNameAsync(name);
        }


        public async Task<ApplicationUser> GetUserAsync(ClaimsPrincipal claimsPrincipal)
        {
            return await userManager.GetUserAsync(claimsPrincipal);
        }

        public async Task<bool> CheckPasswordAsync(ApplicationUser user, string password)
        {
            return await userManager.CheckPasswordAsync(user, password);
        }

        public async Task<IdentityResult> CreateAsync(ApplicationUser user, string password)
        {
           return await userManager.CreateAsync(user, password);
        }

        public async Task<IdentityResult> AddToRoleAsync(ApplicationUser user, string roleName) 
        {
            return await userManager.AddToRoleAsync(user, roleName);
        }
    }
}
