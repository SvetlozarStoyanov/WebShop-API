using Contracts.DataAccess.UnitOfWork;
using Contracts.Services.Entity.ApplicationUsers;
using Microsoft.EntityFrameworkCore;
using Models.Common;
using Models.Common.Enums;
using Models.Dto.Users.Output;

namespace Services.Entity.ApplicationUsers
{
    public class ApplicationUserService : IApplicationUserService
    {
        private readonly IUnitOfWork unitOfWork;

        public ApplicationUserService(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }

        public async Task<bool> IsUserNameTakenAsync(string userName)
        {
            return await unitOfWork.UserRepository.AllAsNoTracking().AnyAsync(u => u.NormalizedUserName == userName.ToUpper());
        }

        public async Task<IEnumerable<string>> GetAllUserNamesAsync()
        {
            var usernames = await unitOfWork.UserRepository.AllAsNoTracking()
                .Select(x => x.UserName)
                .ToListAsync();

            return usernames;
        }

        public async Task<OperationResult<UserProfileDto>> GetUserProfileAsync(string userId)
        {
            var operationResult = new OperationResult<UserProfileDto>();
            var user = await unitOfWork.UserRepository.FindByConditionAsNoTracking(x => x.Id == userId)
                .Select(x => new UserProfileDto()
                {
                    UserName = x.UserName,
                    FirstName = x.FirstName,
                    MiddleName = x.MiddleName,
                    LastName = x.LastName,
                })
                .FirstOrDefaultAsync();

            if (user is null)
            {
                operationResult.AppendError(new Error(ErrorTypes.NotFound, $"User with id: {userId} was not found!"));
                return operationResult;
            }

            operationResult.Data = user;

            return operationResult;
        }
    }
}
