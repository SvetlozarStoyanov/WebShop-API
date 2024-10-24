using Contracts.Repositories.BaseRepository;
using Database.Entities.Products;

namespace Contracts.Repositories.Products
{
    public interface IProductRepository : IBaseRepository<long,Product>
    {
    }
}
