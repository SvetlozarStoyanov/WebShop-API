using Contracts.DataAccess.Repositories.Common.Nomenclatures.Orders;
using DataAccess.Repositories.BaseRepository;
using Database;
using Database.Entities.Common.Nomenclatures.Orders;

namespace DataAccess.Repositories.Common.Nomenclatures.Statuses
{
    public class OrderDetailsStageRepository : BaseRepository<long, OrderDetailsStage>, IOrderDetailsStageRepository
    {
        public OrderDetailsStageRepository(WebShopDbContext context) : base(context)
        {
        }
    }
}
