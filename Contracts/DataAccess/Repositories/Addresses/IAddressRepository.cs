using Contracts.DataAccess.Repositories.BaseRepository;
using Database.Entities.Addresses;

namespace Contracts.DataAccess.Repositories.Addresses
{
    public interface IAddressRepository : IBaseRepository<long, Address>
    {
    }
}
