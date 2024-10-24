using Contracts.Repositories.BaseRepository;
using Database.Entities.PhoneNumbers;

namespace Contracts.Repositories.PhoneNumbers
{
    public interface IPhoneNumberRepository : IBaseRepository<long, PhoneNumber>
    {
    }
}
