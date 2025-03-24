using Models.Common;
using Models.Dto.Addresses.Input;
using Models.Dto.Customers.Output;
using Models.Dto.Emails.Input;
using Models.Dto.PhoneNumbers;

namespace Contracts.Services.Managers.Customers
{
    public interface ICustomerManager
    {
        Task<OperationResult> UpdateCustomerAddressesAsync(string userId, IEnumerable<AddressUpdateDto> addressEditDtos);
        Task<OperationResult> UpdateCustomerPhoneNumbersAsync(string userId, IEnumerable<PhoneNumberUpdateDto> updatePhoneNumbersDto);
        Task<OperationResult> UpdateCustomerEmailsAsync(string userId, IEnumerable<EmailUpdateDto> emailUpdateDtos);
        Task<OperationResult<CustomerDetailsDto>> GetCustomerDetailsAsync(string userId);
    }
}
