using Contracts.Services.Entity.PhoneNumbers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WebShop.Extensions;

namespace WebShop.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class PhoneNumbersController : ControllerBase
    {
        private readonly IPhoneNumberService phoneNumberService;

        public PhoneNumbersController(IPhoneNumberService phoneNumberService)
        {
            this.phoneNumberService = phoneNumberService;
        }
        
        [HttpGet]
        [Route("get-customer-phone-numbers")]
        public async Task<IActionResult> GetCustomerPhoneNumbersAsync()
        {
            var operationResult = await phoneNumberService.GetCustomerPhoneNumbersAsync(User.GetId());

            if (!operationResult.IsSuccessful)
            {
                return this.Error(operationResult);
            }

            return Ok(operationResult.Data);
        }
    }
}
