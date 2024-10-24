using Contracts.DataAccess.Repositories.Common.Statuses;
using DataAccess.Repositories.BaseRepository;
using Database;
using Database.Entities.Common.Statuses;

namespace DataAccess.Repositories.Common.Statuses
{
    public class OrderStatusRepository : BaseRepository<long, OrderStatus>, IOrderStatusRepository
    {
        public OrderStatusRepository(WebShopDbContext context) : base(context)
        {
        }
    }
}
