using Models.Common;

namespace Contracts.Services.ProfilePictures
{
    public interface IProfilePictureService
    {
        Task<OperationResult> UpdateProfilePictureAsync(string userId, string? profilePicture);

        Task<OperationResult<string?>> GetProfilePictureAsync(string userId);
    }
}
