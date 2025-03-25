using Database.Entities.Addresses;
using Database.Entities.Customers;
using Models.Common;
using Models.Dto.Addresses.Input;
using Models.Dto.Addresses.Output;

namespace Contracts.Services.Entity.Addresses
{
    public interface IAddressService
    {
        /// <summary>
        /// Returns all <see cref="Customer.Addresses"/> of <see cref="Customer"/> with userId - <paramref name="userId"/>
        /// </summary>
        /// <param name="userId"></param>
        /// <returns><see cref="OperationResult"/> with data <see cref="IEnumerable"/> of <see cref="AddressDetailsDto"/></returns>
        Task<OperationResult<IEnumerable<AddressDetailsDto>>> GetCustomerAddressesAsync(string userId);

        /// <summary>
        /// Updates <paramref name="addresses"/> with data from <paramref name="addressUpdateDtos"/>
        /// </summary>
        /// <param name="addresses"></param>
        /// <param name="addressUpdateDtos"></param>
        /// <returns><see cref="OperationResult"/> result</returns>
        Task<OperationResult> UpdateCustomerAddressesAsync(ICollection<Address> addresses, IEnumerable<AddressUpdateDto> addressUpdateDtos);
    }
}
