using Models.Common;
using Models.Dto.Addresses.Input;
using Models.Dto.Customers;
using Models.Dto.Emails.Input;
using Models.Dto.PhoneNumbers.Input;

namespace Contracts.Services.Managers.Customers
{
    public interface ICustomerManager
    {
        Task<OperationResult> UpdateCustomerAddressesAsync(string userId, IEnumerable<AddressEditDto> addressEditDtos);
        Task<OperationResult> UpdateCustomerPhoneNumbersAsync(string userId, UpdatePhoneNumbersDto updatePhoneNumbersDto);
        Task<OperationResult> UpdateCustomerEmailsAsync(string userId, UpdateEmailsDto updateEmailsDto);
    }
}
