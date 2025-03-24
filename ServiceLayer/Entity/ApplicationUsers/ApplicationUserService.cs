using Contracts.DataAccess.UnitOfWork;
using Contracts.Services.Entity.ApplicationUsers;
using Microsoft.EntityFrameworkCore;
using Models.Common;
using Models.Common.Enums;
using Models.Dto.Addresses.Output;
using Models.Dto.Customers.Output;
using Models.Dto.Emails.Output;
using Models.Dto.PhoneNumbers.Output;

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

    }
}
