using Database.Entities.Inventory;
using Database.Entities.Orders;
using Models.Common;

namespace Contracts.Services.Entity.InventoryTransactions
{
    public interface IInventoryTransactionService
    {
        /// <summary>
        /// Creates <see cref="InventoryTransaction"/> with data from <paramref name="order"/>
        /// </summary>
        /// <param name="order"></param>
        /// <returns><see cref="OperationResult"/> result</returns>
        Task<OperationResult> CreateInventoryTransactionsFromOrderAsync(Order order);

        /// <summary>
        /// Creates <see cref="InventoryTransaction"/> with data from <paramref name="order"/>
        /// </summary>
        /// <param name="order"></param>
        /// <returns><see cref="OperationResult"/> result</returns>
        Task<OperationResult> CreateInventoryTransactionsFromCancelledOrderAsync(Order order);
    }
}
