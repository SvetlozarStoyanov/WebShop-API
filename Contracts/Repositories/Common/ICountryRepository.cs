using Contracts.Repositories.BaseRepository;
using Database.Entities.Common;

namespace Contracts.Repositories.Common
{
    public interface ICountryRepository : IBaseRepository<long, Country>
    {
    }
}
