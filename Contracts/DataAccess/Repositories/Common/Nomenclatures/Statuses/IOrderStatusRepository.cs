using Contracts.DataAccess.Repositories.BaseRepository;
using Database.Entities.Common.Nomenclatures.Statuses;

namespace Contracts.DataAccess.Repositories.Common.Nomenclatures.Statuses
{
    public interface IOrderStatusRepository : IBaseRepository<long, OrderStatus>
    {
    }
}
