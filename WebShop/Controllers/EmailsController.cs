using Contracts.Services.Entity.Emails;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WebShop.Extensions;

namespace WebShop.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class EmailsController : ControllerBase
    {
        private readonly IEmailService emailService;

        public EmailsController(IEmailService emailService)
        {
            this.emailService = emailService;
        }


        [HttpGet]
        [Route("get-customer-emails")]
        public async Task<IActionResult> GetCustomerEmails()
        {
            var operationResult = await emailService.GetCustomerEmailsAsync(User.GetId());

            if (!operationResult.IsSuccessful)
            {
                return this.Error(operationResult);
            }

            return Ok(operationResult.Data);
        }
    }
}
