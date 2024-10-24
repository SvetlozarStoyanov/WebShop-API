using Contracts.Repositories.Common.Statuses;
using DataAccess.Repositories.BaseRepository;
using Database;
using Database.Entities.Common.Statuses;

namespace DataAccess.Repositories.Common.Statuses
{
    public class DiscountStatusRepository : BaseRepository<long, DiscountStatus>, IDiscountStatusRepository
    {
        public DiscountStatusRepository(WebShopDbContext context) : base(context)
        {
        }
    }
}
