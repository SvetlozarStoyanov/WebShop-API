using Contracts.DataAccess.Repositories.BaseRepository;
using Database.Entities.Common.Types;

namespace Contracts.DataAccess.Repositories.Common.Types
{
    public interface IInventoryTransactionTypeRepository : IBaseRepository<long, InventoryTransactionType>
    {
    }
}
