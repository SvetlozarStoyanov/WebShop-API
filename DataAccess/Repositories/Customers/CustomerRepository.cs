using Contracts.DataAccess.Repositories.Customers;
using DataAccess.Repositories.BaseRepository;
using Database;
using Database.Entities.Common.Enums.Statuses;
using Database.Entities.Customers;
using Microsoft.EntityFrameworkCore;

namespace DataAccess.Repositories.Customers
{
    public class CustomerRepository : BaseRepository<long, Customer>, ICustomerRepository
    {
        public CustomerRepository(WebShopDbContext context) : base(context)
        {
        }

        public async Task<Customer?> GetCustomerWithPersonalDetailsAsync(string userId)
        {
            return await FindByCondition(x => x.UserId == userId)
                .Include(x => x.Addresses.Where(y => y.StatusId == (long)AddressStatuses.Active))
                .ThenInclude(x => x.Country)
                .Include(x => x.Emails.Where(y => y.StatusId == (long)EmailStatuses.Active))
                .Include(x => x.PhoneNumbers.Where(y => y.StatusId == (long)PhoneNumberStatuses.Active))
                .ThenInclude(x => x.Country)
                .FirstOrDefaultAsync();
        }
    }
}
