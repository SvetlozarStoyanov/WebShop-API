using Database.Entities.Emails;
using Models.Common;
using Models.Dto.Emails.Output;
using Models.Dto.Emails.Input;

namespace Contracts.Services.Entity.Emails
{
    public interface IEmailService
    {
        Task<OperationResult<IEnumerable<EmailDetailsDto>>> GetCustomerEmailsAsync(string userId);
        Task<OperationResult> UpdateCustomerEmailsAsync(ICollection<Email> emails, IEnumerable<EmailUpdateDto> emailUpdateDtos);
    }
}
