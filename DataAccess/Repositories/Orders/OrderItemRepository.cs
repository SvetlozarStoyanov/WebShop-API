using Contracts.DataAccess.Repositories.Orders;
using DataAccess.Repositories.BaseRepository;
using Database;
using Database.Entities.Orders;

namespace DataAccess.Repositories.Orders
{
    public class OrderItemRepository : BaseRepository<long, OrderItem>, IOrderItemRepository
    {
        public OrderItemRepository(WebShopDbContext context) : base(context)
        {
        }
    }
}
