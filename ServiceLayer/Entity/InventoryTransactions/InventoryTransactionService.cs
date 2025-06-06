﻿using Contracts.DataAccess.UnitOfWork;
using Contracts.Services.Entity.InventoryTransactions;
using Database.Entities.Common.Enums.InventoryTransactions;
using Database.Entities.Common.Nomenclatures.InventoryTransactions;
using Database.Entities.Inventory;
using Database.Entities.Orders;
using Models.Common;
using System.Data;

namespace Services.Entity.InventoryTransactions
{
    public class InventoryTransactionService : IInventoryTransactionService
    {
        private readonly IUnitOfWork unitOfWork;

        public InventoryTransactionService(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }

        ///<inheritdoc/>
        public async Task<OperationResult> CreateInventoryTransactionsFromCancelledOrderAsync(Order order)
        {
            var operationResult = new OperationResult();

            var returnedType = await unitOfWork.InventoryTransactionTypeRepository.GetByIdAsync((long)InventoryTransactionTypes.Returned);

            if (returnedType is null)
            {
                throw new InvalidOperationException($"{nameof(InventoryTransactionType)} of type: {InventoryTransactionTypes.Returned} does not exist!");
            }

            foreach (var orderItem in order.Items)
            {
                var inventoryTransaction = new InventoryTransaction
                {
                    Type = returnedType,
                    Quantity = orderItem.Quantity,
                    Date = DateTime.UtcNow,
                };

                orderItem.Product.QuantityInStock += orderItem.Quantity;

                orderItem.Product.InventoryTransactions.Add(inventoryTransaction);
            }

            return operationResult;
        }

        ///<inheritdoc/>
        public async Task<OperationResult> CreateInventoryTransactionsFromOrderAsync(Order order)
        {
            var operationResult = new OperationResult();

            var soldType = await unitOfWork.InventoryTransactionTypeRepository.GetByIdAsync((long)InventoryTransactionTypes.Sold);

            if (soldType is null)
            {
                throw new InvalidOperationException($"{nameof(InventoryTransactionType)} of type: {InventoryTransactionTypes.Sold} does not exist!");
            }

            foreach (var orderItem in order.Items)
            {
                var inventoryTransaction = new InventoryTransaction();

                inventoryTransaction.Quantity = orderItem.Quantity;
                inventoryTransaction.Type = soldType;
                inventoryTransaction.Date = DateTime.UtcNow;

                orderItem.Product.QuantityInStock -= orderItem.Quantity;

                orderItem.Product.InventoryTransactions.Add(inventoryTransaction);
            }

            return operationResult;
        }
    }
}
