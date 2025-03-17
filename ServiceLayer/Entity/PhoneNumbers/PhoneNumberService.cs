using Contracts.DataAccess.UnitOfWork;
using Contracts.Services.Entity.PhoneNumbers;
using Database.Entities.Common.Enums.Statuses;
using Database.Entities.Common.Nomenclatures.Statuses;
using Database.Entities.PhoneNumbers;
using Models.Common;
using Models.Common.Enums;
using Models.Dto.PhoneNumbers;
using Models.Dto.PhoneNumbers.Output;

namespace Services.Entity.PhoneNumbers
{
    public class PhoneNumberService : IPhoneNumberService
    {
        private readonly IUnitOfWork unitOfWork;

        public PhoneNumberService(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }

        public async Task<OperationResult<IEnumerable<PhoneNumberDetailsDto>>> GetCustomerPhoneNumbersAsync(string userId)
        {
            var operationResult = new OperationResult<IEnumerable<PhoneNumberDetailsDto>>();

            var customerWithPhoneNumbers = await unitOfWork.CustomerRepository.GetCustomerWithPhoneNumbersAsync(userId);

            if (customerWithPhoneNumbers is null)
            {
                operationResult.AppendError(new Error(ErrorTypes.NotFound, $"User with id: {userId} was not found!"));
                return operationResult;
            }

            operationResult.Data = customerWithPhoneNumbers.PhoneNumbers.Select(x => new PhoneNumberDetailsDto()
            {
                Id = x.Id,
                Number = x.Number,
                IsMain = x.IsMain,
                CountryId = x.CountryId,
            });

            return operationResult;
        }

        public async Task<OperationResult> UpdateCustomerPhoneNumbersAsync(ICollection<PhoneNumber> phoneNumbers, IEnumerable<PhoneNumberUpdateDto> phoneNumberUpdateDtos)
        {
            var operationResult = new OperationResult();

            var activeStatus = await unitOfWork.PhoneNumberStatusRepository.GetByIdAsync((long)PhoneNumberStatuses.Active);

            if (activeStatus is null)
            {
                throw new InvalidOperationException($"{nameof(PhoneNumberStatus)} of type: {PhoneNumberStatuses.Active} was not found!");
            }

            foreach (var phoneNumberUpdateDto in phoneNumberUpdateDtos)
            {
                if (phoneNumberUpdateDto.Id is null)
                {
                    var phoneNumberCreateOperationResult = await CreatePhoneNumberAsync(phoneNumberUpdateDto, activeStatus);

                    if (!phoneNumberCreateOperationResult.IsSuccessful)
                    {
                        operationResult.AppendErrors(phoneNumberCreateOperationResult);
                        return operationResult;
                    }

                    phoneNumbers.Add(phoneNumberCreateOperationResult.Data);
                }
                else
                {
                    var phoneNumber = phoneNumbers.FirstOrDefault(x => x.Id == phoneNumberUpdateDto.Id);
                    if (phoneNumber is null)
                    {
                        operationResult.AppendError(new Error(ErrorTypes.NotFound, $"{nameof(PhoneNumber)} with id: {phoneNumberUpdateDto.Id} was not found!"));
                        return operationResult;
                    }

                    var updatePhoneNumberOperationResult = await UpdatePhoneNumberAsync(phoneNumberUpdateDto, phoneNumber);
                    if (!updatePhoneNumberOperationResult.IsSuccessful)
                    {
                        operationResult.AppendErrors(updatePhoneNumberOperationResult);
                        return operationResult;
                    }
                }
            }

            var archivedStatus = await unitOfWork.PhoneNumberStatusRepository.GetByIdAsync((long)PhoneNumberStatuses.Archived);

            if (archivedStatus is null)
            {
                throw new InvalidOperationException($"{nameof(PhoneNumberStatus)} of type: {PhoneNumberStatuses.Active} was not found!");
            }
            var updatedPhoneNumberIds = phoneNumberUpdateDtos.Select(x => x.Id);

            var phoneNumberIdsForPhoneNumbersToDelete = phoneNumbers.Where(x => !updatedPhoneNumberIds.Contains(x.Id) && x.Id != 0).Select(x => x.Id);

            foreach (var deletedId in phoneNumberIdsForPhoneNumbersToDelete)
            {
                DeletePhoneNumber(deletedId, archivedStatus, phoneNumbers);
            }

            if (phoneNumbers.Count(x => x.Status.Name == activeStatus.Name && x.IsMain) != 1)
            {
                operationResult.AppendError(new Error(ErrorTypes.BadRequest, $"Must have 1 main {nameof(PhoneNumber)}"));
                return operationResult;
            }

            return operationResult;
        }

        private async Task<OperationResult<PhoneNumber>> CreatePhoneNumberAsync(PhoneNumberUpdateDto phoneNumberUpdateDto, PhoneNumberStatus activeStatus)
        {
            var operationResult = new OperationResult<PhoneNumber>();

            var country = await unitOfWork.CountryRepository.GetByIdAsync(phoneNumberUpdateDto.CountryId);

            if (country is null)
            {
                operationResult.AppendError(new Error(ErrorTypes.NotFound, $"Country with see {phoneNumberUpdateDto.CountryId} was not found!"));
                return operationResult;
            }

            var phoneNumber = new PhoneNumber()
            {
                Number = phoneNumberUpdateDto.Number,
                IsMain = phoneNumberUpdateDto.IsMain,
                Country = country,
                Status = activeStatus,
            };

            operationResult.Data = phoneNumber;

            return operationResult;
        }

        private async Task<OperationResult> UpdatePhoneNumberAsync(PhoneNumberUpdateDto phoneNumberUpdateDto, PhoneNumber phoneNumber)
        {
            var operationResult = new OperationResult();

            var country = await unitOfWork.CountryRepository.GetByIdAsync(phoneNumberUpdateDto.CountryId);

            if (country is null)
            {
                operationResult.AppendError(new Error(ErrorTypes.NotFound, $"Country with id: {phoneNumberUpdateDto.CountryId} was not found!"));
                return operationResult;
            }

            phoneNumber.Number = phoneNumberUpdateDto.Number;
            phoneNumber.IsMain = phoneNumberUpdateDto.IsMain;
            phoneNumber.Country = country;

            return operationResult;
        }

        private OperationResult DeletePhoneNumber(long id, PhoneNumberStatus archivedStatus, IEnumerable<PhoneNumber> phoneNumbers)
        {
            var operationResult = new OperationResult();

            var phoneNumber = phoneNumbers.FirstOrDefault(x => x.Id == id);

            if (phoneNumber is null)
            {
                operationResult.AppendError(new Error(ErrorTypes.NotFound, $"{nameof(PhoneNumber)} with Id - {id} was not found!"));
                return operationResult;
            }

            phoneNumber.Status = archivedStatus;

            return operationResult;
        }
    }
}
