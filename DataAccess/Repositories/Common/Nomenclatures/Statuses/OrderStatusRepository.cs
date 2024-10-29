using Contracts.DataAccess.Repositories.Common.Nomenclatures.Statuses;
using DataAccess.Repositories.BaseRepository;
using Database;
using Database.Entities.Common.Nomenclatures.Statuses;

namespace DataAccess.Repositories.Common.Nomenclatures.Statuses
{
    public class OrderStatusRepository : BaseRepository<long, OrderStatus>, IOrderStatusRepository
    {
        public OrderStatusRepository(WebShopDbContext context) : base(context)
        {
        }
    }
}
