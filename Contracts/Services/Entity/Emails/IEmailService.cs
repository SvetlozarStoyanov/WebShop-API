using Database.Entities.Emails;
using Models.Common;
using Models.Dto.Emails.Output;
using Models.Dto.Emails.Input;
using Database.Entities.Customers;

namespace Contracts.Services.Entity.Emails
{
    public interface IEmailService
    {
        /// <summary>
        /// Returns all <see cref="Customer.Emails"/> of <see cref="Customer"/> with userId - <paramref name="userId"/>
        /// </summary>
        /// <param name="userId"></param>
        /// <returns><see cref="OperationResult"/> with data <see cref="IEnumerable"/> of <see cref="EmailDetailsDto"/></returns>
        Task<OperationResult<IEnumerable<EmailDetailsDto>>> GetCustomerEmailsAsync(string userId);

        /// <summary>
        /// Updates <paramref name="emails"/> with data from <paramref name="emailUpdateDtos"/>
        /// </summary>
        /// <param name="emails"></param>
        /// <param name="emailUpdateDtos"></param>
        /// <returns><see cref="OperationResult"/> result</returns>
        Task<OperationResult> UpdateCustomerEmailsAsync(ICollection<Email> emails, IEnumerable<EmailUpdateDto> emailUpdateDtos);
    }
}
