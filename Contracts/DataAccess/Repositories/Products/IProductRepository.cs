using Contracts.DataAccess.Repositories.BaseRepository;
using Database.Entities.Products;

namespace Contracts.DataAccess.Repositories.Products
{
    public interface IProductRepository : IBaseRepository<long, Product>
    {
    }
}
