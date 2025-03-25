using Database.Entities.Customers;
using Database.Entities.PhoneNumbers;
using Models.Common;
using Models.Dto.PhoneNumbers;
using Models.Dto.PhoneNumbers.Output;

namespace Contracts.Services.Entity.PhoneNumbers
{
    public interface IPhoneNumberService
    {
        /// <summary>
        /// Returns all <see cref="Customer.PhoneNumbers"/> of <see cref="Customer"/> with userId - <paramref name="userId"/>
        /// </summary>
        /// <param name="userId"></param>
        /// <returns><see cref="OperationResult"/> with data <see cref="IEnumerable"/> of <see cref="PhoneNumberDetailsDto"/></returns>
        Task<OperationResult<IEnumerable<PhoneNumberDetailsDto>>> GetCustomerPhoneNumbersAsync(string userId);

        /// <summary>
        /// Updates <paramref name="phoneNumbers"/> with data from <paramref name="phoneNumberUpdateDto"/>
        /// </summary>
        /// <param name="phoneNumbers"></param>
        /// <param name="phoneNumberUpdateDto"></param>
        /// <returns><see cref="OperationResult"/> result</returns>
        Task<OperationResult> UpdateCustomerPhoneNumbersAsync(ICollection<PhoneNumber> phoneNumbers, IEnumerable<PhoneNumberUpdateDto> phoneNumberUpdateDto);
    }
}
