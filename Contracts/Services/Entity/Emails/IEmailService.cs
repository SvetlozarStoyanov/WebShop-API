﻿using Database.Entities.Emails;
using Models.Common;
using Models.Dto.Emails.Input;

namespace Contracts.Services.Entity.Emails
{
    public interface IEmailService
    {
        Task<OperationResult> UpdateCustomerEmailsAsync(ICollection<Email> emails, UpdateEmailsDto updateEmailsDto);
    }
}
