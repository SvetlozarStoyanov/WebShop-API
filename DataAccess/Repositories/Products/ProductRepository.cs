using Contracts.DataAccess.Repositories.Products;
using DataAccess.Repositories.BaseRepository;
using Database;
using Database.Entities.Products;

namespace DataAccess.Repositories.Products
{
    public class ProductRepository : BaseRepository<long, Product>, IProductRepository
    {
        public ProductRepository(WebShopDbContext context) : base(context)
        {
        }
    }
}
