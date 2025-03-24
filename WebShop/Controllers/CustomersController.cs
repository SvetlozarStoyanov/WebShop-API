using Contracts.Services.Managers.Customers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Models.Dto.Addresses.Input;
using Models.Dto.Emails.Input;
using Models.Dto.PhoneNumbers;
using WebShop.Extensions;

namespace WebShop.Controllers
{
    [Route("api/[controller]")]
    [Authorize]
    [ApiController]
    public class CustomersController : ControllerBase
    {
        private readonly ICustomerManager customerManager;

        public CustomersController(ICustomerManager customerManager)
        {
            this.customerManager = customerManager;
        }

        [HttpPut]
        [Route("edit-addresses")]
        public async Task<IActionResult> EditAddresses(IEnumerable<AddressUpdateDto> addressUpdateDtos)
        {
            var operationResult = await customerManager.UpdateCustomerAddressesAsync(User.GetId(), addressUpdateDtos);

            if (!operationResult.IsSuccessful)
            {
                return this.Error(operationResult);
            }

            return Ok();
        }

        [HttpPut]
        [Route("edit-phone-numbers")]
        public async Task<IActionResult> EditPhoneNumbers(IEnumerable<PhoneNumberUpdateDto> phoneNumberUpdateDtos)
        {
            var operationResult = await customerManager.UpdateCustomerPhoneNumbersAsync(User.GetId(), phoneNumberUpdateDtos);

            if (!operationResult.IsSuccessful)
            {
                return this.Error(operationResult);
            }

            return Ok();
        }

        [HttpPut]
        [Route("edit-emails")]
        public async Task<IActionResult> EditEmails(IEnumerable<EmailUpdateDto> emailUpdateDtos)
        {
            var operationResult = await customerManager.UpdateCustomerEmailsAsync(User.GetId(), emailUpdateDtos);

            if (!operationResult.IsSuccessful)
            {
                return this.Error(operationResult);
            }

            return Ok();
        }

        [HttpGet]
        [Route("own-details")]
        public async Task<IActionResult> GetOwnDetails()
        {
            var operationResult = await customerManager.GetCustomerDetailsAsync(User.GetId());

            if (!operationResult.IsSuccessful)
            {
                return this.Error(operationResult);
            }

            return Ok(operationResult.Data);
        }
        
    }
}
