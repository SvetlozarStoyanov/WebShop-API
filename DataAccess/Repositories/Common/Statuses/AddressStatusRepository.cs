using Contracts.DataAccess.Repositories.Common.Statuses;
using DataAccess.Repositories.BaseRepository;
using Database;
using Database.Entities.Common.Statuses;

namespace DataAccess.Repositories.Common.Statuses
{
    public class AddressStatusRepository : BaseRepository<long, AddressStatus>, IAddressStatusRepository
    {
        public AddressStatusRepository(WebShopDbContext context) : base(context)
        {
        }
    }
}
