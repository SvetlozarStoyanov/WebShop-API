using Contracts.DataAccess.Repositories.Common.Types;
using DataAccess.Repositories.BaseRepository;
using Database;
using Database.Entities.Common.Types;

namespace DataAccess.Repositories.Common.Types
{
    public class InventoryTransactionTypeRepository : BaseRepository<long, InventoryTransactionType>, IInventoryTransactionTypeRepository
    {
        public InventoryTransactionTypeRepository(WebShopDbContext context) : base(context)
        {
        }
    }
}
