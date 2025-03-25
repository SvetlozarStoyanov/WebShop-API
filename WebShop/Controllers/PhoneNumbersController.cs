using Contracts.Services.Entity.PhoneNumbers;
using Contracts.Services.Managers.Customers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WebShop.Extensions;

namespace WebShop.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class PhoneNumbersController : ControllerBase
    {
        private readonly ICustomerManager customerManager;

        public PhoneNumbersController(ICustomerManager customerManager)
        {
            this.customerManager = customerManager;
        }
        
        [HttpGet]
        [Route("get-customer-phone-numbers")]
        public async Task<IActionResult> GetCustomerPhoneNumbersAsync()
        {
            var operationResult = await customerManager.GetCustomerPhoneNumbersAsync(User.GetId());

            if (!operationResult.IsSuccessful)
            {
                return this.Error(operationResult);
            }

            return Ok(operationResult.Data);
        }
    }
}
