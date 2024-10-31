using Database.Entities.Identity;
using Microsoft.AspNetCore.Identity;
using System.Security.Claims;

namespace Contracts.Services.Identity
{
    public interface ICustomUserManager
    {
        Task<bool> CheckPasswordAsync(ApplicationUser user, string password);
        Task<ApplicationUser> FindByIdAsync(string id);
        Task<ApplicationUser> FindByNameAsync(string name);
        Task<ApplicationUser> GetUserAsync(ClaimsPrincipal claimsPrincipal);
        Task<IdentityResult> CreateAsync(ApplicationUser user, string password);
        Task<IdentityResult> AddToRoleAsync(ApplicationUser user, string roleName);
    }
}
