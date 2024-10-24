using Contracts.Repositories.Orders;
using DataAccess.Repositories.BaseRepository;
using Database;
using Database.Entities.Orders;

namespace DataAccess.Repositories.Orders
{
    public class OrderRepository : BaseRepository<long, Order>, IOrderRepository
    {
        public OrderRepository(WebShopDbContext context) : base(context)
        {
        }
    }
}
