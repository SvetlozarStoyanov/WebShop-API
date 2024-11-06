using Database.Entities.Orders;
using Models.Common;
using Models.Dto.Orders;

namespace Contracts.Services.Entity.Orders
{
    public interface IOrderService
    {
        Task<OperationResult<Order>> CreateOrderAsync(OrderCreateDto orderCreateDto);

        Task<OperationResult<Order>> CancelOrderAsync(long id);
    }
}
