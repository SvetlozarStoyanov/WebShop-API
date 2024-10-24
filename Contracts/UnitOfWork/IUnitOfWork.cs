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

namespace Contracts.UnitOfWork
{
    public interface IUnitOfWork
    {
        #region Repositories

        #region Common
        public IOrderStatusRepository OrderStatusRepository { get; }
        public IAddressStatusRepository AddressStatusRepository { get; }
        public IPhoneNumberStatusRepository PhoneNumberStatusRepository { get; }
        public IEmailStatusRepository EmailStatusRepository { get; }
        public IDiscountStatusRepository DiscountStatusRepository { get; }
        public IInventoryTransactionTypeRepository InventoryTransactionTypeRepository { get; }
        public IProductTypeRepository ProductTypeRepository { get; }
        public ICountryRepository CountryRepository { get; }
        #endregion
        public IProductRepository ProductRepository { get; }
        public ICustomerRepository CustomerRepository { get; }
        public IOrderRepository OrderRepository { get; }
        public IOrderDetailsRepository OrderDetailsRepository { get; }
        public IDiscountRepository DiscountRepository { get; }
        public IAddressRepository AddressRepository { get; }
        public IEmailRepository EmailRepository { get; }
        public IPhoneNumberRepository PhoneNumberRepository { get; }
        public IInventoryTransactionRepository InventoryTransactionRepository { get; }
        #endregion

        /// <summary>
        /// Saves all made changes in transaction
        /// </summary>
        /// <returns>Number of entities changed</returns>
        public Task<int> SaveChangesAsync();
    }
}
