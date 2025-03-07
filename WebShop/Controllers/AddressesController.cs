using Contracts.Services.Entity.Addresses;
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
        private readonly IAddressService addressService;

        public AddressesController(IAddressService addressService)
        {
            this.addressService = addressService;
        }

        [HttpGet]
        [Route("get-customer-addresses")]
        public async Task<IActionResult> GetCustomerAddresses()
        {
            var operationResult = await addressService.GetCustomerAddressesAsync(User.GetId());

            if (!operationResult.IsSuccessful)
            {
                return this.Error(operationResult);
            }

            return Ok(operationResult.Data);
        }
    }
}
