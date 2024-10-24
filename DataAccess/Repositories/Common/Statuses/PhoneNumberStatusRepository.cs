using Contracts.DataAccess.Repositories.Common.Statuses;
using DataAccess.Repositories.BaseRepository;
using Database;
using Database.Entities.Common.Statuses;

namespace DataAccess.Repositories.Common.Statuses
{
    public class PhoneNumberStatusRepository : BaseRepository<long, PhoneNumberStatus>, IPhoneNumberStatusRepository
    {
        public PhoneNumberStatusRepository(WebShopDbContext context) : base(context)
        {
        }
    }
}
