using Database.Entities.Addresses;
using Models.Common;
using Models.Dto.Addresses.Input;
using Models.Dto.Emails;

namespace Contracts.Services.Entity.Addresses
{
    public interface IAddressService
    {
        Task<OperationResult> UpdateCustomerAddressesAsync(ICollection<Address> addresses, UpdateAddressesDto updateAddressesDto);
    }
}
