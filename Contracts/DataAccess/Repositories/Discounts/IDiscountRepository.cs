using Contracts.DataAccess.Repositories.BaseRepository;
using Database.Entities.Discounts;

namespace Contracts.DataAccess.Repositories.Discounts
{
    public interface IDiscountRepository : IBaseRepository<long, Discount>
    {
    }
}
