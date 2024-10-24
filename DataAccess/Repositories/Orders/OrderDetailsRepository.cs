using Contracts.Repositories.Orders;
using DataAccess.Repositories.BaseRepository;
using Database;
using Database.Entities.Orders;

namespace DataAccess.Repositories.Orders
{
    public class OrderDetailsRepository : BaseRepository<long, OrderDetails>, IOrderDetailsRepository
    {
        public OrderDetailsRepository(WebShopDbContext context) : base(context)
        {
        }
    }
}
