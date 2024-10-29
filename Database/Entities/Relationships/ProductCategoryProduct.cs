using Database.Entities.Common.Nomenclatures.Products;
using Database.Entities.Products;

namespace Database.Entities.Relationships
{
    public class ProductCategoryProduct
    {
        public long Id { get; set; }
        public long ProductTypeId { get; set; }
        public ProductCategory ProductType { get; set; }
        public long ProductId { get; set; }
        public Product Product { get; set; }        
    }
}
