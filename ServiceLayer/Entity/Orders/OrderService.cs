﻿using Contracts.DataAccess.UnitOfWork;
using Contracts.Services.Entity.Orders;
using Database.Entities.Common.Enums.Orders;
using Database.Entities.Common.Enums.Statuses;
using Database.Entities.Common.Nomenclatures.Orders;
using Database.Entities.Common.Nomenclatures.Statuses;
using Database.Entities.Orders;
using Microsoft.EntityFrameworkCore;
using Models.Common;
using Models.Common.Enums;
using Models.Dto.Orders;

namespace Services.Entity.Orders
{
    public class OrderService : IOrderService
    {
        private readonly IUnitOfWork unitOfWork;

        public OrderService(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }

        public async Task<OperationResult<Order>> CreateOrderAsync(OrderCreateDto orderCreateDto)
        {
            var operationResult = new OperationResult<Order>();

            var orderItems = new List<OrderItem>();

            foreach (var orderProduct in orderCreateDto.Products)
            {
                var product = await unitOfWork.ProductRepository
                    .FindByCondition(x => x.Id == orderProduct.ProductId)
                    .Include(x => x.Discounts)
                    .FirstOrDefaultAsync();

                if (product is null)
                {
                    operationResult.AppendError(new Error(ErrorTypes.NotFound, $"Product with id: {orderProduct.ProductId} was not found!"));
                    return operationResult;
                }

                if (product.QuantityInStock < orderProduct.Quantity)
                {
                    operationResult.AppendError(new Error(ErrorTypes.BadRequest, $"Product with id: {product.Id} does not have sufficient quantity!"));
                    return operationResult;
                }

                var activeDiscount = product.Discounts.FirstOrDefault(x => x.StatusId == (long)DiscountStatuses.Active);

                var unitPrice = activeDiscount is not null
                    ? product.Price * activeDiscount.Percentage
                    : product.Price;

                var orderItem = new OrderItem()
                {
                    Product = product,
                    UnitPrice = unitPrice,
                    Quantity = orderProduct.Quantity,
                };

                orderItems.Add(orderItem);
            }


            var shippedStage = await unitOfWork.OrderDetailsStageRepository.GetByIdAsync((long)OrderDetailsStages.Shipped);

            if (shippedStage is null)
            {
                throw new InvalidOperationException($"{nameof(OrderDetailsStage)} of type: {OrderDetailsStages.Shipped} was not found!");
            }

            var orderDetailsActiveStatus = await unitOfWork.OrderDetailsStatusRepository.GetByIdAsync((long)OrderDetailsStatuses.Active);

            if (shippedStage is null)
            {
                throw new InvalidOperationException($"{nameof(OrderDetailsStatus)} of type: {OrderDetailsStatuses.Active} was not found!");
            }

            var orderDetails = new OrderDetails()
            {
                Stage = shippedStage,
                Status = orderDetailsActiveStatus,
                UpdatedOn = DateTime.UtcNow,
            };

            var order = new Order()
            {
                Details = new List<OrderDetails>() { orderDetails },
                Items = orderItems,
            };

            var orderActiveStatus = await unitOfWork.OrderStatusRepository.GetByIdAsync((long)OrderStatuses.Active);

            if (orderActiveStatus is null)
            {
                throw new InvalidOperationException($"{nameof(OrderStatus)} of type: {OrderStatuses.Active} was not found!");
            }

            order.Status = orderActiveStatus;

            await unitOfWork.OrderRepository.AddAsync(order);

            operationResult.Data = order;

            return operationResult;
        }
    }
}