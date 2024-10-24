using Contracts.Repositories.BaseRepository;
using Database.Entities.Orders;

namespace Contracts.Repositories.Orders
{
    public interface IOrderRepository : IBaseRepository<long, Order>
    {
    }
}
