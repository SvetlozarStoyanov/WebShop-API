﻿using Contracts.Repositories.BaseRepository;
using Database.Entities.Common.Types;

namespace Contracts.Repositories.Common.Types
{
    public interface IInventoryTransactionTypeRepository : IBaseRepository<long, InventoryTransactionType>
    {
    }
}
