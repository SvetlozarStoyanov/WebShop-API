using Database.Entities.Addresses;
using Database.Entities.Common;
using Database.Entities.Common.Statuses;
using Database.Entities.Common.Types;
using Database.Entities.Customers;
using Database.Entities.Discounts;
using Database.Entities.Emails;
using Database.Entities.Identity;
using Database.Entities.Inventory;
using Database.Entities.Orders;
using Database.Entities.PhoneNumbers;
using Database.Entities.Products;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Database
{
    public class WebShopDbContext : IdentityDbContext<ApplicationUser>
    {
        public WebShopDbContext(DbContextOptions<WebShopDbContext> options) : base(options)
        {

        }

        #region Common
        public DbSet<Country> Countries { get; set; }
        public DbSet<AddressStatus> AddressStatuses { get; set; }
        public DbSet<EmailStatus> EmailStatuses { get; set; }
        public DbSet<PhoneNumberStatus> PhoneNumberStatuses { get; set; }
        public DbSet<DiscountStatus> DiscountStatuses { get; set; }
        public DbSet<OrderStatus> OrderStatuses { get; set; }
        public DbSet<ProductType> ProductTypes { get; set; }
        public DbSet<InventoryTransactionType> InventoryTransactionTypes { get; set; }

        #endregion

        public DbSet<Customer> Customers { get; set; }
        public DbSet<Email> Emails { get; set; }
        public DbSet<PhoneNumber> PhoneNumbers { get; set; }
        public DbSet<Address> Addresses { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<InventoryTransaction> InventoryTransactions { get; set; }
        public DbSet<Discount> Discounts { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<OrderDetails> OrderDetails { get; set; }
    }
}
