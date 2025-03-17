using Contracts.DataAccess.UnitOfWork;
using Contracts.Services.Entity.Addresses;
using Database.Entities.Addresses;
using Database.Entities.Common.Enums.Statuses;
using Database.Entities.Common.Nomenclatures.Statuses;
using Database.Entities.PhoneNumbers;
using Models.Common;
using Models.Common.Enums;
using Models.Dto.Addresses.Input;
using Models.Dto.Addresses.Output;

namespace Services.Entity.Addresses
{
    public class AddressService : IAddressService
    {
        private readonly IUnitOfWork unitOfWork;

        public AddressService(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }

        public async Task<OperationResult<IEnumerable<AddressDetailsDto>>> GetCustomerAddressesAsync(string userId)
        {
            var operationResult = new OperationResult<IEnumerable<AddressDetailsDto>>();

            var customerWithAddresses = await unitOfWork.CustomerRepository.GetCustomerWithAddressesAsync(userId);

            if (customerWithAddresses is null)
            {
                operationResult.AppendError(new Error(ErrorTypes.NotFound, $"User with id: {userId} was not found!"));
                return operationResult;
            }

            var addresses = customerWithAddresses.Addresses.Select(x => new AddressDetailsDto()
            {
                Id = x.Id,
                AddressLineOne = x.AddressLineOne,
                AddressLineTwo = x.AddressLineTwo,
                City = x.City,
                PostCode = x.PostCode,
                IsMain = x.IsMain,
                CountryId = x.CountryId,
            });

            operationResult.Data = addresses;

            return operationResult;
        }

        public async Task<OperationResult> UpdateCustomerAddressesAsync(ICollection<Address> addresses, IEnumerable<AddressUpdateDto> addressUpdateDtos)
        {
            var operationResult = new OperationResult();

            var activeStatus = await unitOfWork.AddressStatusRepository.GetByIdAsync((long)AddressStatuses.Active);

            if (activeStatus is null)
            {
                throw new InvalidOperationException($"{nameof(AddressStatus)} of type: {AddressStatuses.Active} was not found!");
            }

            foreach (var addressUpdateDto in addressUpdateDtos)
            {
                if (addressUpdateDto.Id is null)
                {
                    var addressCreateOperationResult = await CreateAddressAsync(addressUpdateDto, activeStatus);
                    if (!addressCreateOperationResult.IsSuccessful)
                    {
                        operationResult.AppendErrors(addressCreateOperationResult);
                        return operationResult;
                    }

                    addresses.Add(addressCreateOperationResult.Data);
                }
                else
                {
                    var address = addresses.FirstOrDefault(x => x.Id == addressUpdateDto.Id);
                    if (address is null)
                    {
                        operationResult.AppendError(new Error(ErrorTypes.NotFound, $"Address with id: {addressUpdateDto.Id} was not found!"));
                        return operationResult;
                    }

                    var updateAddressOperationResult = await EditAddressAsync(addressUpdateDto, address);
                    if (!updateAddressOperationResult.IsSuccessful)
                    {
                        operationResult.AppendErrors(updateAddressOperationResult);
                        return operationResult;
                    }
                }
            }

            var updatedAddressesIds = addressUpdateDtos.Select(x => x.Id);

            var addressIdsForAddressesToDelete = addresses.Where(x => !updatedAddressesIds.Contains(x.Id) && x.Id != 0).Select(x => x.Id);

            var archivedStatus = await unitOfWork.AddressStatusRepository.GetByIdAsync((long)AddressStatuses.Archived);

            if (archivedStatus is null)
            {
                throw new InvalidOperationException($"{nameof(AddressStatus)} of type: {AddressStatuses.Archived} was not found!");
            }

            foreach (var deletedId in addressIdsForAddressesToDelete)
            {
                DeleteAddress(deletedId, archivedStatus, addresses);
            }

            if (addresses.Count(x => x.Status.Name == activeStatus.Name && x.IsMain) != 1)
            {
                operationResult.AppendError(new Error(ErrorTypes.BadRequest, $"Must have 1 main {nameof(Address)}"));
                return operationResult;
            }

            return operationResult;
        }

        private async Task<OperationResult<Address>> CreateAddressAsync(AddressUpdateDto updateDto, AddressStatus activeStatus)
        {
            var operationResult = new OperationResult<Address>();

            var country = await unitOfWork.CountryRepository.GetByIdAsync(updateDto.CountryId);

            if (country is null)
            {
                operationResult.AppendError(new Error(ErrorTypes.NotFound, $"Country with see {updateDto.CountryId} was not found!"));
                return operationResult;
            }

            var address = new Address()
            {
                AddressLineOne = updateDto.AddressLineOne,
                AddressLineTwo = updateDto.AddressLineTwo,
                IsMain = updateDto.IsMain,
                City = updateDto.City,
                PostCode = updateDto.PostCode,
                Country = country,
                Status = activeStatus,
            };

            operationResult.Data = address;

            return operationResult;
        }

        private async Task<OperationResult> EditAddressAsync(AddressUpdateDto addressUpdateDto, Address address)
        {
            var operationResult = new OperationResult();

            var country = await unitOfWork.CountryRepository.GetByIdAsync(addressUpdateDto.CountryId);

            if (country is null)
            {
                operationResult.AppendError(new Error(ErrorTypes.NotFound, $"Country with see {addressUpdateDto.CountryId} was not found!"));
                return operationResult;
            }

            address.AddressLineOne = addressUpdateDto.AddressLineOne;
            address.AddressLineTwo = addressUpdateDto.AddressLineTwo;
            address.IsMain = addressUpdateDto.IsMain;
            address.City = addressUpdateDto.City;
            address.PostCode = addressUpdateDto.PostCode;
            address.Country = country;

            return operationResult;
        }

        private OperationResult DeleteAddress(long id, AddressStatus archivedStatus, ICollection<Address> addresses)
        {
            var operationResult = new OperationResult();

            var address = addresses.FirstOrDefault(x => x.Id == id);

            if (address is null)
            {
                operationResult.AppendError(new Error(ErrorTypes.NotFound, $"{nameof(Address)} with Id - {id} was not found!"));
                return operationResult;
            }

            address.Status = archivedStatus;

            return operationResult;
        }
    }
}
