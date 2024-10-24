using Contracts.DataAccess.Repositories.Customers;
using DataAccess.Repositories.BaseRepository;
using Database;
using Database.Entities.Customers;

namespace DataAccess.Repositories.Customers
{
    public class CustomerRepository : BaseRepository<long, Customer>, ICustomerRepository
    {
        public CustomerRepository(WebShopDbContext context) : base(context)
        {
        }
    }
}
