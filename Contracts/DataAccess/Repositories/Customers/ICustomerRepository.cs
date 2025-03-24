using Contracts.DataAccess.Repositories.BaseRepository;
using Database.Entities.Customers;

namespace Contracts.DataAccess.Repositories.Customers
{
    public interface ICustomerRepository : IBaseRepository<long, Customer>
    {
        Task<Customer?> GetCustomerWithOrdersAsync(string userId);
        Task<Customer?> GetCustomerWithAddressesAsync(string userId);
        Task<Customer?> GetCustomerWithPhoneNumbersAsync(string userId);
        Task<Customer?> GetCustomerWithEmailsAsync(string userId);
        Task<Customer?> GetCustomerWithPersonalDetailsAsync(string userId);
        Task<Customer?> GetCustomerWithPersonalDetailsAsNoTrackingAsync(string userId);

    }
}
