using Contracts.DataAccess.Repositories.Common.Nomenclatures.Products;
using DataAccess.Repositories.BaseRepository;
using Database;
using Database.Entities.Common.Nomenclatures.Products;

namespace DataAccess.Repositories.Common.Nomenclatures.Products
{
    public class ProductCategoryRepository : BaseRepository<long, ProductCategory>, IProductCategoryRepository
    {
        public ProductCategoryRepository(WebShopDbContext context) : base(context)
        {
        }
    }
}
