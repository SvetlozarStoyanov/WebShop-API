using Database.Entities.Customers;
using Database.Entities.Orders;
using Models.Common;
using Models.Dto.Orders.Input;

namespace Contracts.Services.Managers.Orders
{
    /// <summary>
    /// Performs <see cref="Order"/> related operations
    /// </summary>
    public interface IOrderManager
    {
        /// <summary>
        /// Creates an <see cref="Order"/>, for <see cref="Customer"/>
        /// with <see cref="Customer.UserId"/> :  <paramref name="userId"/>,
        /// with data from <paramref name="orderCreateDto"/>
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="orderCreateDto"></param>
        /// <returns><see cref="OperationResult"/> result</returns>
        Task<OperationResult> PlaceOrderAsync(string userId, OrderCreateDto orderCreateDto);

        /// <summary>
        /// Cancels <see cref="Order"/> with <see cref="Order.Id"/>: <paramref name="orderId"/>
        /// if it belongs to <see cref="Customer"/> with <see cref="Customer.UserId"/>: <paramref name="userId"/>
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="orderId"></param>
        /// <returns></returns>
        Task<OperationResult> CancelOrderAsync(string userId, long orderId);
    }
}
