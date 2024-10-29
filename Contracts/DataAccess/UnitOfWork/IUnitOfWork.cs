using Contracts.DataAccess.Repositories.Addresses;
using Contracts.DataAccess.Repositories.ApplicationUsers;
using Contracts.DataAccess.Repositories.Common;
using Contracts.DataAccess.Repositories.Common.Nomenclatures.Orders;
using Contracts.DataAccess.Repositories.Common.Nomenclatures.Products;
using Contracts.DataAccess.Repositories.Common.Nomenclatures.Statuses;
using Contracts.DataAccess.Repositories.Common.Nomenclatures.Types;
using Contracts.DataAccess.Repositories.Customers;
using Contracts.DataAccess.Repositories.Discounts;
using Contracts.DataAccess.Repositories.Emails;
using Contracts.DataAccess.Repositories.InventoryTransactions;
using Contracts.DataAccess.Repositories.Orders;
using Contracts.DataAccess.Repositories.PhoneNumbers;
using Contracts.DataAccess.Repositories.Products;

namespace Contracts.DataAccess.UnitOfWork
{
    public interface IUnitOfWork
    {
        #region Repositories

        #region Common
        public IOrderStatusRepository OrderStatusRepository { get; }
        public IOrderDetailsStatusRepository OrderDetailsStatusRepository { get; }
        public IOrderDetailsStageRepository OrderDetailsStageRepository { get; }
        public IAddressStatusRepository AddressStatusRepository { get; }
        public IPhoneNumberStatusRepository PhoneNumberStatusRepository { get; }
        public IEmailStatusRepository EmailStatusRepository { get; }
        public IDiscountStatusRepository DiscountStatusRepository { get; }
        public IInventoryTransactionTypeRepository InventoryTransactionTypeRepository { get; }
        public IProductCategoryRepository ProductTypeRepository { get; }
        public ICountryRepository CountryRepository { get; }
        #endregion
        public IApplicationUserRepository UserRepository { get;}
        public IProductRepository ProductRepository { get; }
        public ICustomerRepository CustomerRepository { get; }
        public IOrderRepository OrderRepository { get; }
        public IOrderDetailsRepository OrderDetailsRepository { get; }
        public IOrderItemRepository OrderItemRepository { get; }
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
