using Contracts.DataAccess.Repositories.BaseRepository;
using Database.Entities.Orders;

namespace Contracts.DataAccess.Repositories.Orders
{
    public interface IOrderRepository : IBaseRepository<long, Order>
    {
    }
}
