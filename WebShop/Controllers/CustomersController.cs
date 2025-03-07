using Contracts.Services.Managers.Customers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Models.Dto.Addresses.Input;
using Models.Dto.Customers;
using Models.Dto.Emails.Input;
using Models.Dto.PhoneNumbers.Input;
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
        public async Task<IActionResult> EditAddresses(IEnumerable<AddressEditDto> addressEditDtos)
        {
            var operationResult = await customerManager.UpdateCustomerAddressesAsync(User.GetId(), addressEditDtos);

            if (!operationResult.IsSuccessful)
            {
                return this.Error(operationResult);
            }

            return Ok();
        }

        [HttpPut]
        [Route("edit-phone-numbers")]
        public async Task<IActionResult> EditPhoneNumbers(UpdatePhoneNumbersDto updatePhoneNumbersDto)
        {
            var operationResult = await customerManager.UpdateCustomerPhoneNumbersAsync(User.GetId(), updatePhoneNumbersDto);

            if (!operationResult.IsSuccessful)
            {
                return this.Error(operationResult);
            }

            return Ok();
        }

        [HttpPut]
        [Route("edit-emails")]
        public async Task<IActionResult> EditEmails(UpdateEmailsDto updateEmailsDto)
        {
            var operationResult = await customerManager.UpdateCustomerEmailsAsync(User.GetId(), updateEmailsDto);

            if (!operationResult.IsSuccessful)
            {
                return this.Error(operationResult);
            }

            return Ok();
        }
        
    }
}
