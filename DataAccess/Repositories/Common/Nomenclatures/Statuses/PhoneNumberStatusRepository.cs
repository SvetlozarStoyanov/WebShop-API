using Contracts.DataAccess.Repositories.Common.Nomenclatures.Statuses;
using DataAccess.Repositories.BaseRepository;
using Database;
using Database.Entities.Common.Nomenclatures.Statuses;

namespace DataAccess.Repositories.Common.Nomenclatures.Statuses
{
    public class PhoneNumberStatusRepository : BaseRepository<long, PhoneNumberStatus>, IPhoneNumberStatusRepository
    {
        public PhoneNumberStatusRepository(WebShopDbContext context) : base(context)
        {
        }
    }
}
