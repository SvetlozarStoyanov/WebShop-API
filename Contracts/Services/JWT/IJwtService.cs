using Database.Entities.Identity;

namespace Contracts.Services.JWT
{
    /// <summary>
    /// Handles working with JWT Tokens
    /// </summary>
    public interface IJwtService
    {
        /// <summary>
        /// Generates a JWT Token for an <see cref="ApplicationUser"/> with <paramref name="userId"/>
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="userName"></param>
        /// <returns><see cref="string"/> jwtToken</returns>
        string GenerateJwtToken(string userId, string userName);
    }
}
