using Contracts.DataAccess.Repositories.Common.Nomenclatures.Statuses;
using DataAccess.Repositories.BaseRepository;
using Database;
using Database.Entities.Common.Nomenclatures.Statuses;

namespace DataAccess.Repositories.Common.Nomenclatures.Statuses
{
    public class DiscountStatusRepository : BaseRepository<long, DiscountStatus>, IDiscountStatusRepository
    {
        public DiscountStatusRepository(WebShopDbContext context) : base(context)
        {
        }
    }
}
