using Database.Entities.PhoneNumbers;
using Models.Common;
using Models.Dto.PhoneNumbers;
using Models.Dto.PhoneNumbers.Output;

namespace Contracts.Services.Entity.PhoneNumbers
{
    public interface IPhoneNumberService
    {
        Task<OperationResult<IEnumerable<PhoneNumberDetailsDto>>> GetCustomerPhoneNumbersAsync(string userId);
        Task<OperationResult> UpdateCustomerPhoneNumbersAsync(ICollection<PhoneNumber> phoneNumbers, IEnumerable<PhoneNumberUpdateDto> phoneNumberUpdateDto);
    }
}
