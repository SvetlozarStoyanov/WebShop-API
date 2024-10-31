using Contracts.Services.Managers.Customers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Models.Dto.Customers;
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
        [Route("edit")]
        public async Task<IActionResult> EditCustomer(CustomerEditDto customerEditDto)
        {
            var operationResult = await customerManager.UpdateCustomerAsync(User.GetId(), customerEditDto);

            if (!operationResult.IsSuccessful)
            {
                return this.Error(operationResult);
            }

            return Ok();
        }


    }
}
