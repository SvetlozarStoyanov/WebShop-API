using Contracts.DataAccess.Repositories.BaseRepository;
using Database.Entities.Customers;

namespace Contracts.DataAccess.Repositories.Customers
{
    public interface ICustomerRepository : IBaseRepository<long, Customer>
    {
        Task<Customer?> GetCustomerWithOrdersAsync(string userId);
        Task<Customer?> GetCustomerWithPersonalDetailsAsync(string userId);
    }
}
