using Contracts.DataAccess.Repositories.BaseRepository;
using Database.Entities.Common;

namespace Contracts.DataAccess.Repositories.Common
{
    public interface ICountryRepository : IBaseRepository<long, Country>
    {
    }
}
