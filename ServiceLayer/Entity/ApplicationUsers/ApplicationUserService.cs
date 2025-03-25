using Contracts.DataAccess.UnitOfWork;
using Contracts.Services.Entity.ApplicationUsers;
using Microsoft.EntityFrameworkCore;

namespace Services.Entity.ApplicationUsers
{
    
    public class ApplicationUserService : IApplicationUserService
    {
        private readonly IUnitOfWork unitOfWork;

        public ApplicationUserService(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }

        ///<inheritdoc/>
        public async Task<bool> IsUserNameTakenAsync(string userName)
        {
            return await unitOfWork.UserRepository.AllAsNoTracking().AnyAsync(u => u.NormalizedUserName == userName.ToUpper());
        }

        ///<inheritdoc/>
        public async Task<IEnumerable<string>> GetAllUserNamesAsync()
        {
            var usernames = await unitOfWork.UserRepository.AllAsNoTracking()
                .Select(x => x.UserName)
                .ToListAsync();

            return usernames;
        }

    }
}
