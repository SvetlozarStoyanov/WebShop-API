using Contracts.DataAccess.UnitOfWork;
using Contracts.Services.Entity.Emails;
using Database.Entities.Addresses;
using Database.Entities.Common.Enums.Statuses;
using Database.Entities.Common.Nomenclatures.Statuses;
using Database.Entities.Emails;
using Models.Common;
using Models.Common.Enums;
using Models.Dto.Addresses;
using Models.Dto.Emails.Input;

namespace Services.Entity.Emails
{
    public class EmailService : IEmailService
    {
        private readonly IUnitOfWork unitOfWork;

        public EmailService(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }

        public async Task<OperationResult> UpdateCustomerEmailsAsync(ICollection<Email> emails, UpdateEmailsDto updateEmailsDto)
        {
            var operationResult = new OperationResult();

            if (updateEmailsDto.CreatedEmails.Any())
            {
                operationResult.AppendErrors(await CreateEmailsAsync(emails, updateEmailsDto.CreatedEmails));
                if (!operationResult.IsSuccessful)
                {
                    return operationResult;
                }
            }

            if (updateEmailsDto.EditedEmails.Any())
            {
                operationResult.AppendErrors(EditEmails(emails, updateEmailsDto.EditedEmails));
                if (!operationResult.IsSuccessful)
                {
                    return operationResult;
                }
            }

            if (updateEmailsDto.DeletedEmails.Any())
            {
                operationResult.AppendErrors(await DeleteEmailsAsync(emails, updateEmailsDto.DeletedEmails));
                if (!operationResult.IsSuccessful)
                {
                    return operationResult;
                }
            }

            if (emails.Where(x => x.Status.Name == EmailStatuses.Active.ToString()).Count(x => x.IsMain) != 1)
            {
                operationResult.AppendError(new Error(ErrorTypes.BadRequest, $"Can only have only 1 main {nameof(Email)}!"));
                return operationResult;
            }

            if (emails.DistinctBy(x => x.Address).Count() != emails.Count)
            {
                operationResult.AppendError(new Error(ErrorTypes.BadRequest, "Cannot have multiple emails with same address!"));
                return operationResult;
            }

            return operationResult;
        }

        private async Task<OperationResult> CreateEmailsAsync(ICollection<Email> emails, IEnumerable<EmailCreateDto> createDtos)
        {
            var operationResult = new OperationResult();

            var activeStatus = await unitOfWork.EmailStatusRepository.GetByIdAsync((long)EmailStatuses.Active);

            if (activeStatus is null)
            {
                throw new InvalidOperationException($"{nameof(AddressStatus)} of type: {AddressStatuses.Active} was not found!");
            }

            foreach (var createDto in createDtos)
            {
                var email = new Email()
                {
                    Address = createDto.Address,
                    IsMain = createDto.IsMain,
                    Status = activeStatus,
                };

                emails.Add(email);
            }

            return operationResult;
        }

        private OperationResult EditEmails(ICollection<Email> emails, IEnumerable<EmailEditDto> editDtos)
        {
            var operationResult = new OperationResult();

            foreach (var editDto in editDtos)
            {
                var email = emails.FirstOrDefault(a => a.Id == editDto.Id);

                if (email is null)
                {
                    operationResult.AppendError(new Error(ErrorTypes.NotFound, $"{nameof(Address)} with Id - {editDto.Id} was not found!"));
                    return operationResult;
                }

                email.Address = editDto.Address;
                email.IsMain = editDto.IsMain;
            }

            return operationResult;
        }

        private async Task<OperationResult> DeleteEmailsAsync(ICollection<Email> emails, IEnumerable<EmailDeleteDto> deleteDtos)
        {
            var operationResult = new OperationResult();

            var archivedStatus = await unitOfWork.EmailStatusRepository.GetByIdAsync((long)EmailStatuses.Archived);

            if (archivedStatus is null)
            {
                throw new InvalidOperationException($"{nameof(AddressStatus)} of type: {AddressStatuses.Active} was not found!");
            }

            foreach (var deleteDto in deleteDtos)
            {
                var address = emails.FirstOrDefault(x => x.Id == deleteDto.Id);

                if (address is null)
                {
                    operationResult.AppendError(new Error(ErrorTypes.NotFound, $"{nameof(Email)} with Id - {deleteDto.Id} was not found!"));
                    return operationResult;
                }

                address.Status = archivedStatus;
            }

            return operationResult;
        }
    }
}
