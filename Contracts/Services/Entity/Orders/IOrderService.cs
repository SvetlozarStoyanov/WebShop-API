using Database.Entities.Orders;
using Models.Common;
using Models.Dto.Orders.Input;

namespace Contracts.Services.Entity.Orders
{
    public interface IOrderService
    {
        /// <summary>
        /// Creates <see cref="Order"/> with data from <paramref name="orderCreateDto"/>
        /// </summary>
        /// <param name="orderCreateDto"></param>
        /// <returns><see cref="OperationResult"/> with data - <see cref="Order"/></returns>
        Task<OperationResult<Order>> CreateOrderAsync(OrderCreateDto orderCreateDto);

        /// <summary>
        /// Cancels <see cref="Order"/> with <paramref name="id"/>
        /// </summary>
        /// <param name="id"></param>
        /// <returns><see cref="OperationResult"/> with data - <see cref="Order"/></returns>
        Task<OperationResult<Order>> CancelOrderAsync(long id);
    }
}
