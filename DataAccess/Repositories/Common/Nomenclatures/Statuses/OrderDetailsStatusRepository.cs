using Contracts.DataAccess.Repositories.Common.Nomenclatures.Statuses;
using DataAccess.Repositories.BaseRepository;
using Database;
using Database.Entities.Common.Nomenclatures.Statuses;

namespace DataAccess.Repositories.Common.Nomenclatures.Statuses
{
    public class OrderDetailsStatusRepository : BaseRepository<long, OrderDetailsStatus>, IOrderDetailsStatusRepository
    {
        public OrderDetailsStatusRepository(WebShopDbContext context) : base(context)
        {
        }
    }
}
