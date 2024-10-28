using Contracts.DataAccess.Repositories.ApplicationUsers;
using DataAccess.Repositories.BaseRepository;
using Database;
using Database.Entities.Identity;

namespace DataAccess.Repositories.ApplicationUsers
{
    public class ApplicationUserRepository : BaseRepository<string, ApplicationUser>, IApplicationUserRepository
    {
        public ApplicationUserRepository(WebShopDbContext context) : base(context)
        {
        }
    }
}
