using Contracts.DataAccess.Repositories.BaseRepository;
using Database.Entities.Common.Nomenclatures.Orders;

namespace Contracts.DataAccess.Repositories.Common.Nomenclatures.Orders
{
    public interface IOrderDetailsStageRepository : IBaseRepository<long, OrderDetailsStage>
    {
    }
}
