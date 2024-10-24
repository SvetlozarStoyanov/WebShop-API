using Contracts.Repositories.Discounts;
using DataAccess.Repositories.BaseRepository;
using Database;
using Database.Entities.Discounts;

namespace DataAccess.Repositories.Discounts
{
    public class DiscountRepository : BaseRepository<long, Discount>, IDiscountRepository
    {
        public DiscountRepository(WebShopDbContext context) : base(context)
        {
        }
    }
}
