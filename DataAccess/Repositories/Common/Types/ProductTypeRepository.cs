using Contracts.Repositories.Common.Types;
using DataAccess.Repositories.BaseRepository;
using Database;
using Database.Entities.Common.Types;

namespace DataAccess.Repositories.Common.Types
{
    public class ProductTypeRepository : BaseRepository<long, ProductType>, IProductTypeRepository
    {
        public ProductTypeRepository(WebShopDbContext context) : base(context)
        {
        }
    }
}
