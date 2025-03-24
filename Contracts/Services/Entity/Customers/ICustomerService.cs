using Database.Entities.Customers;
using Database.Entities.Identity;
using Models.Common;
using Models.Dto.Customers.Input;
using Models.Dto.Customers.Output;

namespace Contracts.Services.Entity.Customers
{
    public interface ICustomerService
    {
        Task<OperationResult> CreateCustomerAsync(ApplicationUser user, CustomerRegisterDto customerRegisterDto);

        Task<OperationResult<Customer>> GetCustomerWithOrdersAsync(string userId);
        Task<OperationResult<Customer>> GetCustomerWithAddressesAsync(string userId);
        Task<OperationResult<Customer>> GetCustomerWithPhoneNumbersAsync(string userId);
        Task<OperationResult<Customer>> GetCustomerWithEmailsAsync(string userId);

        Task<OperationResult<CustomerDetailsDto>> GetCustomerDetailsAsync(string userId);

    }
}
