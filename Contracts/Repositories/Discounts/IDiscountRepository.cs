using Contracts.Repositories.BaseRepository;
using Database.Entities.Discounts;

namespace Contracts.Repositories.Discounts
{
    public interface IDiscountRepository : IBaseRepository<long, Discount>
    {
    }
}
