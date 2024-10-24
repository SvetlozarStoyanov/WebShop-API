using Database.Entities.Common.Types;
using Database.Entities.Discounts;

namespace Database.Entities.Products
{
    public class Product
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public int QuantityInStock { get; set; }
        public decimal Price { get; set; }
        public string Base64Image { get; set; }
        public long TypeId { get; set; }
        public ProductType Type { get; set; }
        public ICollection<Discount> Discounts { get; set; }
    }
}
