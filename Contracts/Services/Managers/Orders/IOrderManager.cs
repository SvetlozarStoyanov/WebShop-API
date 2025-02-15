using Models.Common;
using Models.Dto.Orders.Input;

namespace Contracts.Services.Managers.Orders
{
    public interface IOrderManager
    {
        Task<OperationResult> PlaceOrderAsync(string userId, OrderCreateDto orderCreateDto);

        Task<OperationResult> CancelOrderAsync(string userId, long orderId);
    }
}
