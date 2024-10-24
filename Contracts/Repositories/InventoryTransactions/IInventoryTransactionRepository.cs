using Contracts.Repositories.BaseRepository;
using Database.Entities.Inventory;

namespace Contracts.Repositories.InventoryTransactions
{
    public interface IInventoryTransactionRepository : IBaseRepository<long, InventoryTransaction>
    {
    }
}
