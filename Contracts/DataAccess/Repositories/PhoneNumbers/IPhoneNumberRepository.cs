using Contracts.DataAccess.Repositories.BaseRepository;
using Database.Entities.PhoneNumbers;

namespace Contracts.DataAccess.Repositories.PhoneNumbers
{
    public interface IPhoneNumberRepository : IBaseRepository<long, PhoneNumber>
    {
    }
}
