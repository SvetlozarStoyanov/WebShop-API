using Contracts.Repositories.BaseRepository;
using Database.Entities.Customers;

namespace Contracts.Repositories.Customers
{
    public interface ICustomerRepository : IBaseRepository<long, Customer>
    {
    }
}
