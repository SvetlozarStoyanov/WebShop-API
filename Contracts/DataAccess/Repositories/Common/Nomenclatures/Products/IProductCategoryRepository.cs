using Contracts.DataAccess.Repositories.BaseRepository;
using Database.Entities.Common.Nomenclatures.Products;

namespace Contracts.DataAccess.Repositories.Common.Nomenclatures.Products
{
    public interface IProductCategoryRepository : IBaseRepository<long, ProductCategory>
    {
    }
}
