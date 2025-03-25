using Contracts.Services.Entity.Addresses;
using Contracts.Services.Managers.Customers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WebShop.Extensions;

namespace WebShop.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class AddressesController : ControllerBase
    {
        private readonly ICustomerManager customerManager;

        public AddressesController(ICustomerManager customerManager)
        {
            this.customerManager = customerManager;
        }

        [HttpGet]
        [Route("get-customer-addresses")]
        public async Task<IActionResult> GetCustomerAddresses()
        {
            var operationResult = await customerManager.GetCustomerAddressesAsync(User.GetId());

            if (!operationResult.IsSuccessful)
            {
                return this.Error(operationResult);
            }

            return Ok(operationResult.Data);
        }
    }
}
