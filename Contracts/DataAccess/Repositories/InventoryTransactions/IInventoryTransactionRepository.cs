using Contracts.DataAccess.Repositories.BaseRepository;
using Database.Entities.Inventory;

namespace Contracts.DataAccess.Repositories.InventoryTransactions
{
    public interface IInventoryTransactionRepository : IBaseRepository<long, InventoryTransaction>
    {
    }
}
