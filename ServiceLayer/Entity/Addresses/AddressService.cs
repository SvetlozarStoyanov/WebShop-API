using Contracts.DataAccess.UnitOfWork;
using Contracts.Services.Entity.Addresses;
using Database.Entities.Addresses;
using Database.Entities.Common.Enums.Statuses;
using Database.Entities.Common.Nomenclatures.Statuses;
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

        public async Task<OperationResult> UpdateCustomerAddressesAsync(ICollection<Address> addresses, IEnumerable<AddressEditDto> addressEditDtos)
        {
            var operationResult = new OperationResult();

            var activeStatus = await unitOfWork.AddressStatusRepository.GetByIdAsync((long)AddressStatuses.Active);

            if (activeStatus is null)
            {
                throw new InvalidOperationException($"{nameof(AddressStatus)} of type: {AddressStatuses.Active} was not found!");
            }

            foreach (var addressEditDto in addressEditDtos)
            {
                if (addressEditDto.Id is null)
                {
                    var addressCreateOperationResult = await CreateAddressAsync(addressEditDto, activeStatus);
                    if (!addressCreateOperationResult.IsSuccessful)
                    {
                        operationResult.AppendErrors(addressCreateOperationResult);
                        return operationResult;
                    }

                    addresses.Add(addressCreateOperationResult.Data);
                }
                else
                {
                    var address = addresses.FirstOrDefault(x => x.Id == addressEditDto.Id);
                    if (address is null)
                    {
                        operationResult.AppendError(new Error(ErrorTypes.NotFound, $"Address with id: {addressEditDto.Id} was not found!"));
                        return operationResult;
                    }

                    var editAddressOperationResult = await EditAddressAsync(address, addressEditDto);
                    if (!editAddressOperationResult.IsSuccessful)
                    {
                        operationResult.AppendErrors(editAddressOperationResult);
                        return operationResult;
                    }
                }
            }

            var addressIdsForAddressesToDelete = addresses.Where(x => addressEditDtos.All(y => y.Id != x.Id)).Select(x => x.Id);

            var archivedStatus = await unitOfWork.AddressStatusRepository.GetByIdAsync((long)AddressStatuses.Archived);

            if (archivedStatus is null)
            {
                throw new InvalidOperationException($"{nameof(AddressStatus)} of type: {AddressStatuses.Archived} was not found!");
            }

            foreach (var deletedId in addressIdsForAddressesToDelete)
            {
                DeleteAddress(deletedId, archivedStatus, addresses);
            }

            return operationResult;
        }

        private async Task<OperationResult<Address>> CreateAddressAsync(AddressEditDto editDto, AddressStatus activeStatus)
        {
            var operationResult = new OperationResult<Address>();

            var country = await unitOfWork.CountryRepository.GetByIdAsync(editDto.CountryId);

            if (country is null)
            {
                operationResult.AppendError(new Error(ErrorTypes.NotFound, $"Country with see {editDto.CountryId} was not found!"));
                return operationResult;
            }

            var address = new Address()
            {
                AddressLineOne = editDto.AddressLineOne,
                AddressLineTwo = editDto.AddressLineTwo,
                IsMain = editDto.IsMain,
                City = editDto.City,
                PostCode = editDto.PostCode,
                Country = country,
                Status = activeStatus,
            };

            operationResult.Data = address;

            return operationResult;
        }

        private async Task<OperationResult> EditAddressAsync(Address address, AddressEditDto editDto)
        {
            var operationResult = new OperationResult();

            var country = await unitOfWork.CountryRepository.GetByIdAsync(editDto.CountryId);

            if (country is null)
            {
                operationResult.AppendError(new Error(ErrorTypes.NotFound, $"Country with see {editDto.CountryId} was not found!"));
                return operationResult;
            }

            address.AddressLineOne = editDto.AddressLineOne;
            address.AddressLineTwo = editDto.AddressLineTwo;
            address.IsMain = editDto.IsMain;
            address.City = editDto.City;
            address.PostCode = editDto.PostCode;
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
