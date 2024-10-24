using Contracts.DataAccess.Repositories.BaseRepository;
using Database.Entities.Orders;

namespace Contracts.DataAccess.Repositories.Orders
{
    public interface IOrderDetailsRepository : IBaseRepository<long, OrderDetails>
    {
    }
}
