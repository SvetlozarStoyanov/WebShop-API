﻿using Contracts.DataAccess.Repositories.BaseRepository;
using Database.Entities.Common.Nomenclatures.Statuses;

namespace Contracts.DataAccess.Repositories.Common.Nomenclatures.Statuses
{
    public interface IAddressStatusRepository : IBaseRepository<long, AddressStatus>
    {
    }
}
