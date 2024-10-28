using Contracts.DataAccess.Repositories.BaseRepository;
using Database.Entities.Identity;

namespace Contracts.DataAccess.Repositories.ApplicationUsers
{
    public interface IApplicationUserRepository : IBaseRepository<string, ApplicationUser>
    {
    }
}
