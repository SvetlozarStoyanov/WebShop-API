using Contracts.DataAccess.UnitOfWork;
using Contracts.Services.Entity.Addresses;
using Database.Entities.Addresses;
using Database.Entities.Common.Enums.Statuses;
using Database.Entities.Common.Nomenclatures.Statuses;
using Models.Common;
using Models.Common.Enums;
using Models.Dto.Addresses;

namespace Services.Entity.Addresses
{
    public class AddressService : IAddressService
    {
        private readonly IUnitOfWork unitOfWork;

        public AddressService(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }

        public async Task<OperationResult> UpdateCustomerAddressesAsync(ICollection<Address> addresses, UpdateAddressesDto updateAddressesDto)
        {
            var operationResult = new OperationResult();

            if (updateAddressesDto.CreatedAddresses.Any())
            {
                operationResult.AppendErrors(await CreateAddressesAsync(addresses, updateAddressesDto.CreatedAddresses));
                if (!operationResult.IsSuccessful)
                {
                    return operationResult;
                }
            }

            if (updateAddressesDto.EditedAddresses.Any())
            {
                operationResult.AppendErrors(await EditAddressesAsync(addresses, updateAddressesDto.EditedAddresses));
                if (!operationResult.IsSuccessful)
                {
                    return operationResult;
                }
            }

            if (updateAddressesDto.DeletedAddresses.Any())
            {
                operationResult.AppendErrors(await DeleteAddressesAsync(addresses, updateAddressesDto.DeletedAddresses));
                if (!operationResult.IsSuccessful)
                {
                    return operationResult;
                }
            }

            if (addresses.Where(x => x.Status.Name == AddressStatuses.Active.ToString()).Count(x => x.IsMain) != 1)
            {
                operationResult.AppendError(new Error(ErrorTypes.BadRequest, $"Can only have only 1 main {nameof(Address)}!"));
                return operationResult;
            }


            return operationResult;
        }

        private async Task<OperationResult> CreateAddressesAsync(ICollection<Address> addresses, IEnumerable<AddressCreateDto> createDtos)
        {
            var operationResult = new OperationResult();

            var activeStatus = await unitOfWork.AddressStatusRepository.GetByIdAsync((long)AddressStatuses.Active);

            if (activeStatus is null)
            {
                throw new InvalidOperationException($"{nameof(AddressStatus)} of type: {AddressStatuses.Active} was not found!");
            }

            foreach (var createDto in createDtos)
            {
                var country = await unitOfWork.CountryRepository.GetByIdAsync(createDto.CountryId);

                if (country is null)
                {
                    operationResult.AppendError(new Error(ErrorTypes.NotFound, $"Country with see {createDto.CountryId} was not found!"));
                    return operationResult;
                }

                var address = new Address()
                {
                    AddressLineOne = createDto.AddressLineOne,
                    AddressLineTwo = createDto.AddressLineTwo,
                    IsMain = createDto.IsMain,
                    City = createDto.City,
                    PostCode = createDto.PostCode,
                    Country = country,
                    Status = activeStatus,
                };

                addresses.Add(address);
            }

            return operationResult;
        }

        private async Task<OperationResult> EditAddressesAsync(ICollection<Address> addresses, IEnumerable<AddressEditDto> editDtos)
        {
            var operationResult = new OperationResult();

            foreach (var editDto in editDtos)
            {
                var address = addresses.FirstOrDefault(a => a.Id == editDto.Id);

                if (address is null)
                {
                    operationResult.AppendError(new Error(ErrorTypes.NotFound, $"{nameof(Address)} with Id - {editDto.Id} was not found!"));
                    return operationResult;
                }

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
            }

            return operationResult;
        }

        private async Task<OperationResult> DeleteAddressesAsync(ICollection<Address> addresses, IEnumerable<AddressDeleteDto> deleteDtos)
        {
            var operationResult = new OperationResult();


            var archivedStatus = await unitOfWork.AddressStatusRepository.GetByIdAsync((long)AddressStatuses.Archived);

            if (archivedStatus is null)
            {
                throw new InvalidOperationException($"{nameof(AddressStatus)} of type: {AddressStatuses.Active} was not found!");
            }

            foreach (var deleteDto in deleteDtos)
            {
                var address = addresses.FirstOrDefault(x => x.Id == deleteDto.Id);

                if (address is null)
                {
                    operationResult.AppendError(new Error(ErrorTypes.NotFound, $"{nameof(Address)} with Id - {deleteDto.Id} was not found!"));
                    return operationResult;
                }

                address.Status = archivedStatus;
            }

            return operationResult;
        }

    }
}
