using Database.Entities.Customers;
using Database.Entities.Identity;
using Models.Common;
using Models.Dto.Customers.Input;
using Models.Dto.Customers.Output;

namespace Contracts.Services.Entity.Customers
{
    public interface ICustomerService
    {
        /// <summary>
        /// Returns <see cref="Customer"/> with <see cref="Customer.Orders"/>
        /// </summary>
        /// <param name="userId"></param>
        /// <returns><see cref="OperationResult"/> with data - <see cref="Customer"/></returns>
        Task<OperationResult<Customer>> GetCustomerWithOrdersAsync(string userId);

        /// <summary>
        /// Returns <see cref="Customer"/> with <see cref="Customer.Addresses"/>
        /// </summary>
        /// <param name="userId"></param>
        /// <returns><see cref="OperationResult"/> with data - <see cref="Customer"/></returns>
        Task<OperationResult<Customer>> GetCustomerWithAddressesAsync(string userId);

        /// <summary>
        /// Returns <see cref="Customer"/> with <see cref="Customer.PhoneNumbers"/>
        /// </summary>
        /// <param name="userId"></param>
        /// <returns><see cref="OperationResult"/> with data - <see cref="Customer"/></returns>
        Task<OperationResult<Customer>> GetCustomerWithPhoneNumbersAsync(string userId);

        /// <summary>
        /// Returns <see cref="Customer"/> with <see cref="Customer.Emails"/>
        /// </summary>
        /// <param name="userId"></param>
        /// <returns><see cref="OperationResult"/> with data - <see cref="Customer"/></returns>
        Task<OperationResult<Customer>> GetCustomerWithEmailsAsync(string userId);

        /// <summary>
        /// Returns <see cref="Customer"/> with <see cref="Customer.Addresses"/>,
        /// <see cref="Customer.PhoneNumbers"/> and <see cref="Customer.Emails"/>
        /// </summary>
        /// <param name="userId"></param>
        /// <returns><see cref="OperationResult"/> with data - <see cref="Customer"/></returns>
        Task<OperationResult<CustomerDetailsDto>> GetCustomerDetailsAsync(string userId);

        /// <summary>
        /// Creates a <see cref="Customer"/> with data from <paramref name="customerRegisterDto"/> and <paramref name="user"/>
        /// </summary>
        /// <param name="user"></param>
        /// <param name="customerRegisterDto"></param>
        /// <returns><see cref="OperationResult"/> result</returns>
        Task<OperationResult> CreateCustomerAsync(ApplicationUser user, CustomerRegisterDto customerRegisterDto);
    }
}
