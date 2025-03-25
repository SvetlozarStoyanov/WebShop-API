using Contracts.DataAccess.UnitOfWork;
using Contracts.Services.Entity.Emails;
using Database.Entities.Common.Enums.Statuses;
using Database.Entities.Common.Nomenclatures.Statuses;
using Database.Entities.Emails;
using Models.Common;
using Models.Common.Enums;
using Models.Dto.Emails.Input;
using Models.Dto.Emails.Output;

namespace Services.Entity.Emails
{
    public class EmailService : IEmailService
    {
        private readonly IUnitOfWork unitOfWork;

        public EmailService(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }

        ///<inheritdoc/>
        public async Task<OperationResult<IEnumerable<EmailDetailsDto>>> GetCustomerEmailsAsync(string userId)
        {
            var operationResult = new OperationResult<IEnumerable<EmailDetailsDto>>();

            var customerWithEmails = await unitOfWork.CustomerRepository.GetCustomerWithEmailsAsync(userId);

            if (customerWithEmails is null)
            {
                operationResult.AppendError(new Error(ErrorTypes.NotFound, $"User with id: {userId} was not found!"));
                return operationResult;
            }

            operationResult.Data = customerWithEmails.Emails.Select(x => new EmailDetailsDto()
            {
                Id = x.Id,
                Address = x.Address,
                IsMain = x.IsMain
            });

            return operationResult;
        }

        ///<inheritdoc/>
        public async Task<OperationResult> UpdateCustomerEmailsAsync(ICollection<Email> emails, IEnumerable<EmailUpdateDto> emailUpdateDtos)
        {
            var operationResult = new OperationResult();

            var activeStatus = await unitOfWork.EmailStatusRepository.GetByIdAsync((long)EmailStatuses.Active);

            if (activeStatus is null)
            {
                throw new InvalidOperationException($"{nameof(EmailStatus)} of type: {EmailStatuses.Active} was not found!");
            }

            foreach (var emailUpdateDto in emailUpdateDtos)
            {
                if (emailUpdateDto.Id is null)
                {
                    var emailCreateOperationResult = CreateEmail(emailUpdateDto, activeStatus);
                    if (!emailCreateOperationResult.IsSuccessful)
                    {
                        operationResult.AppendErrors(emailCreateOperationResult);
                        return operationResult;
                    }

                    emails.Add(emailCreateOperationResult.Data);
                }
                else
                {
                    var email = emails.FirstOrDefault(x => x.Id == emailUpdateDto.Id);
                    if (email is null)
                    {
                        operationResult.AppendError(new Error(ErrorTypes.NotFound, $"Email with id: {emailUpdateDto.Id} was not found!"));
                        return operationResult;
                    }

                    var updateEmailOperationResult = UpdateEmail(emailUpdateDto, email);
                    if (!updateEmailOperationResult.IsSuccessful)
                    {
                        operationResult.AppendErrors(updateEmailOperationResult);
                        return operationResult;
                    }
                }
            }

            var editedEmailIds = emailUpdateDtos.Select(x => x.Id);

            var emailIdsForEmailsToDelete = emails.Where(x => !editedEmailIds.Contains(x.Id) && x.Id != 0).Select(x => x.Id);

            var archivedStatus = await unitOfWork.EmailStatusRepository.GetByIdAsync((long)EmailStatuses.Archived);

            if (archivedStatus is null)
            {
                throw new InvalidOperationException($"{nameof(EmailStatus)} of type: {EmailStatuses.Archived} was not found!");
            }

            foreach (var deletedId in emailIdsForEmailsToDelete)
            {
                DeleteEmail(deletedId, archivedStatus, emails);
            }

            if (emails.Count(x => x.Status.Name == activeStatus.Name && x.IsMain) != 1)
            {
                operationResult.AppendError(new Error(ErrorTypes.BadRequest, $"Must have 1 main {nameof(Email)}"));
                return operationResult;
            }

            return operationResult;
        }

        private OperationResult<Email> CreateEmail(EmailUpdateDto emailUpdateDto, EmailStatus status)
        {
            var operationResult = new OperationResult<Email>();

            var email = new Email()
            {
                Address = emailUpdateDto.Address,
                IsMain = emailUpdateDto.IsMain,
                Status = status,
            };

            operationResult.Data = email;

            return operationResult;
        }

        private OperationResult UpdateEmail(EmailUpdateDto emailUpdateDto, Email email)
        {
            var operationResult = new OperationResult();

            email.Address = emailUpdateDto.Address;
            email.IsMain = emailUpdateDto.IsMain;

            return operationResult;
        }

        private OperationResult DeleteEmail(long id, EmailStatus archivedStatus, ICollection<Email> emails)
        {
            var operationResult = new OperationResult();

            var email = emails.FirstOrDefault(x => x.Id == id);

            if (email is null)
            {
                operationResult.AppendError(new Error(ErrorTypes.NotFound, $"{nameof(Email)} with Id - {id} was not found!"));
                return operationResult;
            }

            email.Status = archivedStatus;

            return operationResult;
        }
    }
}
