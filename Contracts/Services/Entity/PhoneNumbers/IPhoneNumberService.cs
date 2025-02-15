using Database.Entities.PhoneNumbers;
using Models.Common;
using Models.Dto.PhoneNumbers.Input;

namespace Contracts.Services.Entity.PhoneNumbers
{
    public interface IPhoneNumberService
    {
        Task<OperationResult> UpdateCustomerPhoneNumbersAsync(ICollection<PhoneNumber> phoneNumbers, UpdatePhoneNumbersDto updatePhoneNumbersDto);
    }
}
