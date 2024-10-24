using Contracts.DataAccess.Repositories.BaseRepository;
using Database.Entities.Common.Statuses;

namespace Contracts.DataAccess.Repositories.Common.Statuses
{
    public interface IOrderStatusRepository : IBaseRepository<long, OrderStatus>
    {
    }
}
