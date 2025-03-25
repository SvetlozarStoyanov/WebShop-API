using Database.Entities.Identity;

namespace Contracts.Services.Entity.ApplicationUsers
{
    public interface IApplicationUserService
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
    }
}
