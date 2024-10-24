using Contracts.DataAccess.Repositories.InventoryTransactions;
using DataAccess.Repositories.BaseRepository;
using Database;
using Database.Entities.Inventory;

namespace DataAccess.Repositories.InventoryTransactions
{
    public class InventoryTransactionRepository : BaseRepository<long, InventoryTransaction>, IInventoryTransactionRepository
    {
        public InventoryTransactionRepository(WebShopDbContext context) : base(context)
        {
        }
    }
}
