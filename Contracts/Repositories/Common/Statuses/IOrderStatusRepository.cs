using Contracts.Repositories.BaseRepository;
using Database.Entities.Common.Statuses;

namespace Contracts.Repositories.Common.Statuses
{
    public interface IOrderStatusRepository : IBaseRepository<long, OrderStatus>
    {
    }
}
