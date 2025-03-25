using Database.Entities.Customers;
using Database.Entities.Identity;
using Models.Common;
using Models.Dto.Addresses.Input;
using Models.Dto.Addresses.Output;
using Models.Dto.Customers.Input;
using Models.Dto.Customers.Output;
using Models.Dto.Emails.Input;
using Models.Dto.Emails.Output;
using Models.Dto.PhoneNumbers;
using Models.Dto.PhoneNumbers.Output;

namespace Contracts.Services.Managers.Customers
{
    /// <summary>
    /// Performs <see cref="Customer"/> related operations
    /// </summary>
    public interface ICustomerManager
    {
        /// <summary>
        /// Returns all <see cref="Customer.Addresses"/> of <see cref="Customer"/> with userId - <paramref name="userId"/>
        /// </summary>
        /// <param name="userId"></param>
        /// <returns><see cref="OperationResult"/> with data <see cref="IEnumerable"/> of <see cref="AddressDetailsDto"/></returns>
        Task<OperationResult<IEnumerable<AddressDetailsDto>>> GetCustomerAddressesAsync(string userId);

        /// <summary>
        /// Returns all <see cref="Customer.PhoneNumbers"/> of <see cref="Customer"/> with userId - <paramref name="userId"/>
        /// </summary>
        /// <param name="userId"></param>
        /// <returns><see cref="OperationResult"/> with data <see cref="IEnumerable"/> of <see cref="PhoneNumberDetailsDto"/></returns>
        Task<OperationResult<IEnumerable<PhoneNumberDetailsDto>>> GetCustomerPhoneNumbersAsync(string userId);

        /// <summary>
        /// Returns all <see cref="Customer.Emails"/> of <see cref="Customer"/> with userId - <paramref name="userId"/>
        /// </summary>
        /// <param name="userId"></param>
        /// <returns><see cref="OperationResult"/> with data <see cref="IEnumerable"/> of <see cref="EmailDetailsDto"/></returns>
        Task<OperationResult<IEnumerable<EmailDetailsDto>>> GetCustomerEmailsAsync(string userId);

        /// <summary>
        /// Updates <see cref="Customer.Addresses"/>, of <see cref="Customer"/> 
        /// with <see cref="Customer.UserId"/> : <paramref name="userId"/>
        /// with data from <paramref name="addressUpdateDtos"/>
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="addressUpdateDtos"></param>
        /// <returns><see cref="OperationResult"/> result</returns>
        Task<OperationResult> UpdateCustomerAddressesAsync(string userId, IEnumerable<AddressUpdateDto> addressUpdateDtos);

        /// <summary>
        /// Updates <see cref="Customer.PhoneNumbers"/>, of <see cref="Customer"/> 
        /// with <see cref="Customer.UserId"/> : <paramref name="userId"/>
        /// with data from <paramref name="phoneNumberUpdateDtos"/>
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="phoneNumberUpdateDtos"></param>
        /// <returns><see cref="OperationResult"/> result</returns>
        
        Task<OperationResult> UpdateCustomerPhoneNumbersAsync(string userId, IEnumerable<PhoneNumberUpdateDto> phoneNumberUpdateDtos);
        /// <summary>
        /// Updates <see cref="Customer.Emails"/>, of <see cref="Customer"/> 
        /// with <see cref="Customer.UserId"/> : <paramref name="userId"/>
        /// with data from <paramref name="emailUpdateDtos"/>
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="emailUpdateDtos"></param>
        /// <returns><see cref="OperationResult"/> result</returns>
        Task<OperationResult> UpdateCustomerEmailsAsync(string userId, IEnumerable<EmailUpdateDto> emailUpdateDtos);

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
