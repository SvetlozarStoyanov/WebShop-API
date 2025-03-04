using Models.Common;
using Models.Dto.Users.Output;

namespace Contracts.Services.Entity.ApplicationUsers
{
    public interface IApplicationUserService
    {
        Task<bool> IsUserNameTakenAsync(string userName);

        Task<IEnumerable<string>> GetAllUserNamesAsync();

        Task<OperationResult<UserProfileDto>> GetUserProfileAsync(string userId);
    }
}
