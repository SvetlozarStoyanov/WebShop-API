using Contracts.Repositories.Common;
using DataAccess.Repositories.BaseRepository;
using Database;
using Database.Entities.Common;

namespace DataAccess.Repositories.Common
{
    public class CountryRepository : BaseRepository<long, Country>, ICountryRepository
    {
        public CountryRepository(WebShopDbContext context) : base(context)
        {
        }
    }
}
