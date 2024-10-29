using Contracts.DataAccess.Repositories.BaseRepository;
using Database.Entities.Common.Nomenclatures.InventoryTransactions;

namespace Contracts.DataAccess.Repositories.Common.Nomenclatures.Types
{
    public interface IInventoryTransactionTypeRepository : IBaseRepository<long, InventoryTransactionType>
    {
    }
}
