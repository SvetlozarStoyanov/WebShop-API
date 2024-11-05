using Contracts.Services.Managers.Orders;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Models.Dto.Orders;
using WebShop.Extensions;

namespace WebShop.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrdersController : ControllerBase
    {
        private readonly IOrderManager orderManager;

        public OrdersController(IOrderManager orderManager)
        {
            this.orderManager = orderManager;
        }

        [Authorize]
        [HttpPost]
        [Route("place-order")]
        public async Task<IActionResult> PlaceOrder(OrderCreateDto orderCreateDto)
        {
            var operationResult = await orderManager.PlaceOrderAsync(User.GetId(), orderCreateDto);

            if (!operationResult.IsSuccessful)
            {
                return this.Error(operationResult);
            }

            return Ok();
        }
    }
}
