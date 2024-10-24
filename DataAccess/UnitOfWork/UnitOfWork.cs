using Contracts.Repositories.Addresses;
using Contracts.Repositories.Common;
using Contracts.Repositories.Common.Statuses;
using Contracts.Repositories.Common.Types;
using Contracts.Repositories.Customers;
using Contracts.Repositories.Discounts;
using Contracts.Repositories.Emails;
using Contracts.Repositories.InventoryTransactions;
using Contracts.Repositories.Orders;
using Contracts.Repositories.PhoneNumbers;
using Contracts.Repositories.Products;
using Contracts.UnitOfWork;
using DataAccess.Repositories.Addresses;
using DataAccess.Repositories.Common;
using DataAccess.Repositories.Common.Statuses;
using DataAccess.Repositories.Common.Types;
using DataAccess.Repositories.Customers;
using DataAccess.Repositories.Discounts;
using DataAccess.Repositories.Emails;
using DataAccess.Repositories.InventoryTransactions;
using DataAccess.Repositories.Orders;
using DataAccess.Repositories.PhoneNumbers;
using DataAccess.Repositories.Products;
using Database;

namespace DataAccess.UnitOfWork
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly WebShopDbContext context;

        // Private repository fields (initially null)
        #region Repository fields

        #region Common
        private IOrderStatusRepository orderStatusRepository;
        private IAddressStatusRepository addressStatusRepository;
        private IPhoneNumberStatusRepository phoneNumberStatusRepository;
        private IEmailStatusRepository emailStatusRepository;
        private IDiscountStatusRepository discountStatusRepository;
        private IInventoryTransactionTypeRepository inventoryTransactionTypeRepository;
        private IProductTypeRepository productTypeRepository;
        private ICountryRepository countryRepository;
        #endregion

        private IProductRepository productRepository;
        private ICustomerRepository customerRepository;
        private IOrderRepository orderRepository;
        private IOrderDetailsRepository orderDetailsRepository;
        private IDiscountRepository discountRepository;
        private IAddressRepository addressRepository;
        private IEmailRepository emailRepository;
        private IPhoneNumberRepository phoneNumberRepository;
        private IInventoryTransactionRepository inventoryTransactionRepository;

        #endregion
        // Constructor to inject the WebShopDbContext
        public UnitOfWork(WebShopDbContext context)
        {
            this.context = context;
        }

        // Implementing the repository properties using the ??= operator for lazy loading
        #region Repositories

        #region Common
        public IOrderStatusRepository OrderStatusRepository => orderStatusRepository ??= new OrderStatusRepository(context);
        public IAddressStatusRepository AddressStatusRepository => addressStatusRepository ??= new AddressStatusRepository(context);
        public IPhoneNumberStatusRepository PhoneNumberStatusRepository => phoneNumberStatusRepository ??= new PhoneNumberStatusRepository(context);
        public IEmailStatusRepository EmailStatusRepository => emailStatusRepository ??= new EmailStatusRepository(context);
        public IDiscountStatusRepository DiscountStatusRepository => discountStatusRepository ??= new DiscountStatusRepository(context);
        public IInventoryTransactionTypeRepository InventoryTransactionTypeRepository => inventoryTransactionTypeRepository ??= new InventoryTransactionTypeRepository(context);
        public IProductTypeRepository ProductTypeRepository => productTypeRepository ??= new ProductTypeRepository(context);
        public ICountryRepository CountryRepository => countryRepository ??= new CountryRepository(context);
        #endregion

        public IProductRepository ProductRepository => productRepository ??= new ProductRepository(context);
        public ICustomerRepository CustomerRepository => customerRepository ??= new CustomerRepository(context);
        public IOrderRepository OrderRepository => orderRepository ??= new OrderRepository(context);
        public IOrderDetailsRepository OrderDetailsRepository => orderDetailsRepository ??= new OrderDetailsRepository(context);
        public IDiscountRepository DiscountRepository => discountRepository ??= new DiscountRepository(context);
        public IAddressRepository AddressRepository => addressRepository ??= new AddressRepository(context);
        public IEmailRepository EmailRepository => emailRepository ??= new EmailRepository(context);
        public IPhoneNumberRepository PhoneNumberRepository => phoneNumberRepository ??= new PhoneNumberRepository(context);
        public IInventoryTransactionRepository InventoryTransactionRepository => inventoryTransactionRepository ??= new InventoryTransactionRepository(context);


        #endregion

        // Implementing the SaveChangesAsync method to save changes to the database
        public async Task<int> SaveChangesAsync()
        {
            return await context.SaveChangesAsync();
        }
    }

}
