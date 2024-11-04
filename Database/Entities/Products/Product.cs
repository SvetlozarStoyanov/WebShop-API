using Database.Entities.Common.Nomenclatures.Products;
using Database.Entities.Discounts;
using Database.Entities.Inventory;

namespace Database.Entities.Products
{
    public class Product
    {
        public Product()
        {
            Categories = new List<ProductCategory>();
            Discounts = new List<Discount>();
            InventoryTransactions = new List<InventoryTransaction>();
        }

        public long Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public int QuantityInStock { get; set; }
        public decimal Price { get; set; }
        public string? Base64Image { get; set; }
        public ICollection<ProductCategory> Categories { get; set; }
        public ICollection<Discount> Discounts { get; set; }
        public ICollection<InventoryTransaction> InventoryTransactions { get; set; }
    }
}
