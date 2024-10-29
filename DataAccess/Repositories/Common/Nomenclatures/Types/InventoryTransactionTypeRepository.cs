using Contracts.DataAccess.Repositories.Common.Nomenclatures.Types;
using DataAccess.Repositories.BaseRepository;
using Database;
using Database.Entities.Common.Nomenclatures.InventoryTransactions;

namespace DataAccess.Repositories.Common.Nomenclatures.Types
{
    public class InventoryTransactionTypeRepository : BaseRepository<long, InventoryTransactionType>, IInventoryTransactionTypeRepository
    {
        public InventoryTransactionTypeRepository(WebShopDbContext context) : base(context)
        {
        }
    }
}
