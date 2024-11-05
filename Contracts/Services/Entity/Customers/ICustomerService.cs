using Database.Entities.Customers;
using Database.Entities.Identity;
using Models.Common;
using Models.Dto.Customers;

namespace Contracts.Services.Entity.Customers
{
    public interface ICustomerService
    {
        Task<OperationResult> CreateCustomerAsync(ApplicationUser user, CustomerRegisterDto customerRegisterDto);

        Task<OperationResult<Customer>> GetCustomerWithOrdersAsync(string userId);

        Task<OperationResult<Customer>> GetCustomerWithPersonalDetailsAsync(string userId);

    }
}
