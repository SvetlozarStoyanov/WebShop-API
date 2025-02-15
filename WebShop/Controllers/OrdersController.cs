using Contracts.Services.Managers.Orders;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Models.Dto.Orders.Input;
using WebShop.Extensions;

namespace WebShop.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class OrdersController : ControllerBase
    {
        private readonly IOrderManager orderManager;

        public OrdersController(IOrderManager orderManager)
        {
            this.orderManager = orderManager;
        }

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

        [HttpPost]
        [Route("cancel-order")]
        public async Task<IActionResult> CancelOrder(long id)
        {
            var operationResult = await orderManager.CancelOrderAsync(User.GetId(), id);

            if (!operationResult.IsSuccessful)
            {
                return this.Error(operationResult);
            }

            return Ok();
        }
    }
}
