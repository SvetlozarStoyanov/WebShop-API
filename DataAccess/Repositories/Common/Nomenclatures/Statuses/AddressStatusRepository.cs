using Contracts.DataAccess.Repositories.Common.Nomenclatures.Statuses;
using DataAccess.Repositories.BaseRepository;
using Database;
using Database.Entities.Common.Nomenclatures.Statuses;

namespace DataAccess.Repositories.Common.Nomenclatures.Statuses
{
    public class AddressStatusRepository : BaseRepository<long, AddressStatus>, IAddressStatusRepository
    {
        public AddressStatusRepository(WebShopDbContext context) : base(context)
        {
        }
    }
}
