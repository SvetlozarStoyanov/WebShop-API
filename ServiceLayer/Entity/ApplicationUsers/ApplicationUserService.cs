using Contracts.DataAccess.UnitOfWork;
using Contracts.Services.Entity.ApplicationUsers;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

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
    }
}
