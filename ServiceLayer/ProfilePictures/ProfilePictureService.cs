using Contracts.DataAccess.UnitOfWork;
using Contracts.Services.ProfilePictures;
using Microsoft.EntityFrameworkCore;
using Models.Common;
using Models.Common.Enums;

namespace Services.ProfilePictures
{
    public class ProfilePictureService : IProfilePictureService
    {
        private readonly IUnitOfWork unitOfWork;

        public ProfilePictureService(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }

        public async Task<OperationResult<string?>> GetProfilePictureAsync(string userId)
        {
            var operationResult = new OperationResult<string?>();
            var user = await unitOfWork.UserRepository.GetByIdAsync(userId);

            if (user is null)
            {
                operationResult.AppendError(new Error(ErrorTypes.BadRequest, $"User with id: {userId} was not found!"));
                return operationResult;
            }

            operationResult.Data = user.ProfilePicture;

            return operationResult;
        }

        public async Task<OperationResult> UpdateProfilePictureAsync(string userId, string profilePicture)
        {
            var operationResult = new OperationResult();
            var user = await unitOfWork.UserRepository.GetByIdAsync(userId);

            if (user is null)
            {
                operationResult.AppendError(new Error(ErrorTypes.BadRequest, $"User with id: {userId} was not found!"));
                return operationResult;
            }
            try
            {
                user.ProfilePicture = profilePicture;
            }
            catch (Exception e)
            {
                var type = e.GetType().Name;
                operationResult.AppendError(new Error(ErrorTypes.InternalServerError, e.Message));
                return operationResult;
            }

            await unitOfWork.SaveChangesAsync();

            return operationResult;
        }
    }
}
