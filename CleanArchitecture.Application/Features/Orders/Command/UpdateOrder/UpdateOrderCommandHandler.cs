using CleanArchitecture.Application.Bases;
using CleanArchitecture.Application.Features.Orders.Rules;
using CleanArchitecture.Application.Interfaces.AutoMapper;
using CleanArchitecture.Application.Interfaces.UnitOfWorks;
using CleanArchitecture.Domain.Entities;
using Microsoft.AspNetCore.Http;
using MediatR;
using CleanArchitecture.Application.Helpers;
using System.Security.Claims;

namespace CleanArchitecture.Application.Features.Orders.Command.UpdateOrder
{
    public class UpdateOrderCommentHandler : BaseHandler, IRequestHandler<UpdateOrderCommandRequest, Unit>
    {
        private readonly OrderRules _orderRules;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public UpdateOrderCommentHandler(IUnitOfWork unitOfWork, IMapper mapper, IHttpContextAccessor httpContextAccessor, OrderRules orderRules)
            : base(unitOfWork, mapper, httpContextAccessor)
        {
            _orderRules = orderRules;
            _httpContextAccessor = httpContextAccessor;
        }

        public async Task<Unit> Handle(UpdateOrderCommandRequest request, CancellationToken cancellationToken)
        {
            LoggerHelper.LogInformation("Handling UpdateOrderRequest for orderId: {orderId}", request.Id);

            var oldOrder = await UnitOfWork.readRepository<Order>()
                                            .GetAsync(predicate: x => x.Id == request.Id);

            if (oldOrder == null)
            {
                LoggerHelper.LogWarning("Order with Id: {OrderId} does not exist", request.Id);
                throw new Exception("Order not found");
            }
            var userId = _httpContextAccessor.HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier);
            await _orderRules.TheOrderShouldBeExist(oldOrder);
            await _orderRules.TheSameUserForTheSameOrder(Guid.Parse(userId), oldOrder.UserId);

            var productsOrders = await UnitOfWork.readRepository<ProductsOrders>()
                                                 .GetAllAsync(predicate: x => x.OrderId == oldOrder.Id);

            LoggerHelper.LogInformation("Deleting existing products for OrderId: {OrderId}", oldOrder.Id);
            await UnitOfWork.writeRepository<ProductsOrders>().DeleteRangeAsync(productsOrders);

            var totalPrice = new List<decimal>();

            foreach (var tempOrder in request.makeOrderDTOs)
            {
                var product = await UnitOfWork.readRepository<Product>()
                                              .GetAsync(predicate: x => x.Id == tempOrder.ProductId);

                if (product == null)
                {
                    LoggerHelper.LogWarning("Product with Id: {ProductId} not found", tempOrder.ProductId);
                    continue;
                }

                totalPrice.Add(product.Price * tempOrder.ProductCount);
                LoggerHelper.LogInformation("ProductId: {ProductId} added to order with Quantity: {Quantity}", tempOrder.ProductId, tempOrder.ProductCount);
            }

            var newOrder = new Order
            {
                Id = request.Id,
                UserId = oldOrder.UserId,
                OrderType = Domain.Enums.OrderType.Received,
                TotalAmount = totalPrice.Sum(),
                AddedOnDate = oldOrder.AddedOnDate,
                UpdatedDate = DateTime.UtcNow
            };

            foreach (var tempOrder in request.makeOrderDTOs)
            {
                await UnitOfWork.writeRepository<ProductsOrders>().AddAsync(new ProductsOrders
                {
                    OrderId = newOrder.Id,
                    ProductId = tempOrder.ProductId
                });
                LoggerHelper.LogInformation("ProductId: {ProductId} added to OrderId: {OrderId}", tempOrder.ProductId, newOrder.Id);
            }

            await UnitOfWork.writeRepository<Order>().UpdateAsync(newOrder.Id, newOrder);

            if (await UnitOfWork.SaveChangeAsync() > 0)
            {
                LoggerHelper.LogInformation("Order with Id: {OrderId} updated successfully", newOrder.Id);
            }
            else
            {
                LoggerHelper.LogWarning("Failed to update OrderId: {OrderId}", newOrder.Id);
            }

            return Unit.Value;
        }
    }

}
