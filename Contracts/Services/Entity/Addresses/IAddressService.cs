using Database.Entities.Addresses;
using Models.Common;
using Models.Dto.Addresses.Input;
using Models.Dto.Addresses.Output;

namespace Contracts.Services.Entity.Addresses
{
    public interface IAddressService
    {
        Task<OperationResult<IEnumerable<AddressDetailsDto>>> GetCustomerAddressesAsync(string userId);
        Task<OperationResult> UpdateCustomerAddressesAsync(ICollection<Address> addresses, IEnumerable<AddressEditDto> addressEditDtos);
    }
}
