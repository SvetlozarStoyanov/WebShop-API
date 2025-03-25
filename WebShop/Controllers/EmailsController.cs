using Contracts.Services.Entity.Emails;
using Contracts.Services.Managers.Customers;
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
        private readonly ICustomerManager customerManager;

        public EmailsController(ICustomerManager customerManager)
        {
            this.customerManager = customerManager;
        }


        [HttpGet]
        [Route("get-customer-emails")]
        public async Task<IActionResult> GetCustomerEmails()
        {
            var operationResult = await customerManager.GetCustomerEmailsAsync(User.GetId());

            if (!operationResult.IsSuccessful)
            {
                return this.Error(operationResult);
            }

            return Ok(operationResult.Data);
        }
    }
}
