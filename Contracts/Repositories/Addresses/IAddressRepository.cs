using Contracts.Repositories.BaseRepository;
using Database.Entities.Addresses;

namespace Contracts.Repositories.Addresses
{
    public interface IAddressRepository : IBaseRepository<long, Address>
    {
    }
}
