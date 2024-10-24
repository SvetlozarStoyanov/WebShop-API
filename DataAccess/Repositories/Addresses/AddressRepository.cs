using Contracts.DataAccess.Repositories.Addresses;
using DataAccess.Repositories.BaseRepository;
using Database;
using Database.Entities.Addresses;

namespace DataAccess.Repositories.Addresses
{
    public class AddressRepository : BaseRepository<long, Address>, IAddressRepository
    {
        public AddressRepository(WebShopDbContext context) : base(context)
        {
        }
    }
}
