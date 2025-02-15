using Contracts.DataAccess.UnitOfWork;
using Contracts.Services.Entity.Customers;
using Contracts.Services.Entity.InventoryTransactions;
using Contracts.Services.Entity.Orders;
using Contracts.Services.Managers.Orders;
using Database.Entities.Customers;
using Database.Entities.Orders;
using Models.Common;
using Models.Common.Enums;
using Models.Dto.Orders.Input;

namespace Services.Managers.Orders
{
    public class OrderManager : IOrderManager
    {
        private readonly IUnitOfWork unitOfWork;
        private readonly IOrderService orderService;
        private readonly IInventoryTransactionService inventoryTransactionService;
        private readonly ICustomerService customerService;

        public OrderManager(IUnitOfWork unitOfWork,
            IOrderService orderService,
            IInventoryTransactionService inventoryTransactionService,
            ICustomerService customerService)
        {
            this.unitOfWork = unitOfWork;
            this.orderService = orderService;
            this.inventoryTransactionService = inventoryTransactionService;
            this.customerService = customerService;
        }

        public async Task<OperationResult> PlaceOrderAsync(string userId, OrderCreateDto orderCreateDto)
        {
            var operationResult = new OperationResult();
            var fetchCustomerResult = await customerService.GetCustomerWithOrdersAsync(userId);

            if (!fetchCustomerResult.IsSuccessful)
            {
                operationResult.AppendErrors(fetchCustomerResult);
                return operationResult;
            }

            var customer = fetchCustomerResult.Data;

            var orderCreateResult = await orderService.CreateOrderAsync(orderCreateDto);

            if (!orderCreateResult.IsSuccessful)
            {
                operationResult.AppendErrors(orderCreateResult);
                return operationResult;
            }

            var order = orderCreateResult.Data;

            var inventoryTranscationsCreateResult = await inventoryTransactionService.CreateInventoryTransactionsFromOrderAsync(order);

            if (!inventoryTranscationsCreateResult.IsSuccessful)
            {
                operationResult.AppendErrors(inventoryTranscationsCreateResult);
                return operationResult;
            }

            customer.Orders.Add(order);

            await unitOfWork.SaveChangesAsync();

            return operationResult;
        }

        public async Task<OperationResult> CancelOrderAsync(string userId, long orderId)
        {
            var operationResult = new OperationResult();

            var customerWithOrdersResult = await customerService.GetCustomerWithOrdersAsync(userId);

            if (!customerWithOrdersResult.IsSuccessful)
            {
                operationResult.AppendErrors(customerWithOrdersResult);
                return operationResult;
            }

            var customerWithOrders = customerWithOrdersResult.Data;

            if (!customerWithOrders.Orders.Any(x => x.Id == orderId))
            {
                operationResult.AppendError(new Error(ErrorTypes.BadRequest, $"{nameof(Customer)} with user id: {userId} does not have an {nameof(Order)} with id: {orderId}"));
                return operationResult;
            }

            var cancelOrderResult = await orderService.CancelOrderAsync(orderId);

            if (!cancelOrderResult.IsSuccessful) 
            {
                operationResult.AppendErrors(cancelOrderResult);
                return operationResult;
            }

            await unitOfWork.SaveChangesAsync();

            return operationResult;
        }
    }
}
