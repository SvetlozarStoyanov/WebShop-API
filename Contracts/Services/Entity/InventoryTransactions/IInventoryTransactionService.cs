using Database.Entities.Orders;
using Models.Common;

namespace Contracts.Services.Entity.InventoryTransactions
{
    public interface IInventoryTransactionService
    {
        Task<OperationResult> CreateInventoryTransactionsFromOrderAsync(Order order);

        Task<OperationResult> CreateInventoryTransactionsFromCancelledOrderAsync(Order order);
    }
}
