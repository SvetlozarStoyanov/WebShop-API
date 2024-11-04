using Database.Entities.Common.Nomenclatures.Products;
using Database.Entities.Products;

namespace Database.Entities.Relationships
{
    public class ProductCategoryProduct
    {
        public long Id { get; set; }
        public long ProductCategoryId { get; set; }
        public ProductCategory ProductCategory { get; set; }
        public long ProductId { get; set; }
        public Product Product { get; set; }        
    }
}
