using Contracts.DataAccess.UnitOfWork;
using Contracts.Services.Entity.PhoneNumbers;
using Database.Entities.Addresses;
using Database.Entities.Common.Enums.Statuses;
using Database.Entities.Common.Nomenclatures.Statuses;
using Database.Entities.Emails;
using Database.Entities.PhoneNumbers;
using Models.Common;
using Models.Common.Enums;
using Models.Dto.Addresses;
using Models.Dto.PhoneNumbers;

namespace Services.Entity.PhoneNumbers
{
    public class PhoneNumberService : IPhoneNumberService
    {
        private readonly IUnitOfWork unitOfWork;

        public PhoneNumberService(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }

        public async Task<OperationResult> UpdateCustomerPhoneNumbersAsync(ICollection<PhoneNumber> phoneNumbers, UpdatePhoneNumbersDto updatePhoneNumbersDto)
        {
            var operationResult = new OperationResult();

            if (updatePhoneNumbersDto.CreatedPhoneNumbers.Any())
            {
                operationResult.AppendErrors(await CreatePhoneNumbersAsync(phoneNumbers, updatePhoneNumbersDto.CreatedPhoneNumbers));
                if (!operationResult.IsSuccessful)
                {
                    return operationResult;
                }
            }

            if (updatePhoneNumbersDto.EditedPhoneNumbers.Any())
            {
                operationResult.AppendErrors(await EditPhoneNumbersAsync(phoneNumbers, updatePhoneNumbersDto.EditedPhoneNumbers));
                if (!operationResult.IsSuccessful)
                {
                    return operationResult;
                }
            }

            if (updatePhoneNumbersDto.DeletedPhoneNumbers.Any())
            {
                operationResult.AppendErrors(await DeleteAddressesAsync(phoneNumbers, updatePhoneNumbersDto.DeletedPhoneNumbers));
                if (!operationResult.IsSuccessful)
                {
                    return operationResult;
                }
            }

            if (phoneNumbers.Where(x => x.Status.Name == PhoneNumberStatuses.Active.ToString()).Count(x => x.IsMain) != 1)
            {
                operationResult.AppendError(new Error(ErrorTypes.BadRequest, $"Can only have only 1 main {nameof(PhoneNumber)}!"));
                return operationResult;
            }

            return operationResult;
        }

        private async Task<OperationResult> CreatePhoneNumbersAsync(ICollection<PhoneNumber> phoneNumbers, IEnumerable<PhoneNumberCreateDto> createDtos)
        {
            var operationResult = new OperationResult();

            var activeStatus = await unitOfWork.PhoneNumberStatusRepository.GetByIdAsync((long)PhoneNumberStatuses.Active);

            if (activeStatus is null)
            {
                throw new InvalidOperationException($"{nameof(PhoneNumberStatus)} of type: {PhoneNumberStatuses.Active} was not found!");
            }

            foreach (var createDto in createDtos)
            {
                var country = await unitOfWork.CountryRepository.GetByIdAsync(createDto.CountryId);

                if (country is null)
                {
                    operationResult.AppendError(new Error(ErrorTypes.NotFound, $"Country with see {createDto.CountryId} was not found!"));
                    return operationResult;
                }

                var phoneNumber = new PhoneNumber()
                {
                    Number = createDto.Number,
                    IsMain = createDto.IsMain,
                    Country = country,
                    Status = activeStatus,
                };

                phoneNumbers.Add(phoneNumber);
            }

            return operationResult;
        }

        private async Task<OperationResult> EditPhoneNumbersAsync(ICollection<PhoneNumber> phoneNumbers, IEnumerable<PhoneNumberEditDto> editDtos)
        {
            var operationResult = new OperationResult();

            foreach (var editDto in editDtos)
            {
                var phoneNumber = phoneNumbers.FirstOrDefault(a => a.Id == editDto.Id);

                if (phoneNumber is null)
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

                phoneNumber.Number = editDto.Number;
                phoneNumber.IsMain = editDto.IsMain;
                phoneNumber.Country = country;
            }

            return operationResult;
        }

        private async Task<OperationResult> DeleteAddressesAsync(ICollection<PhoneNumber> phoneNumbers, IEnumerable<PhoneNumberDeleteDto> deleteDtos)
        {
            var operationResult = new OperationResult();

            var archivedStatus = await unitOfWork.PhoneNumberStatusRepository.GetByIdAsync((long)PhoneNumberStatuses.Archived);

            if (archivedStatus is null)
            {
                throw new InvalidOperationException($"{nameof(PhoneNumberStatus)} of type: {PhoneNumberStatuses.Active} was not found!");
            }

            foreach (var deleteDto in deleteDtos)
            {
                var address = phoneNumbers.FirstOrDefault(x => x.Id == deleteDto.Id);

                if (address is null)
                {
                    operationResult.AppendError(new Error(ErrorTypes.NotFound, $"{nameof(PhoneNumber)} with Id - {deleteDto.Id} was not found!"));
                    return operationResult;
                }

                address.Status = archivedStatus;
            }

            return operationResult;
        }
    }
}
