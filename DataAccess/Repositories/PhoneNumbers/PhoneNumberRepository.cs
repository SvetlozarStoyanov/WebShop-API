using Contracts.DataAccess.Repositories.PhoneNumbers;
using DataAccess.Repositories.BaseRepository;
using Database;
using Database.Entities.PhoneNumbers;

namespace DataAccess.Repositories.PhoneNumbers
{
    public class PhoneNumberRepository : BaseRepository<long, PhoneNumber>, IPhoneNumberRepository
    {
        public PhoneNumberRepository(WebShopDbContext context) : base(context)
        {
        }
    }
}
