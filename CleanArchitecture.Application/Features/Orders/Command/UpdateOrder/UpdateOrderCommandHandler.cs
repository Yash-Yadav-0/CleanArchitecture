using CleanArchitecture.Application.Bases;
using CleanArchitecture.Application.Features.Orders.Rules;
using CleanArchitecture.Application.Interfaces.AutoMapper;
using CleanArchitecture.Application.Interfaces.UnitOfWorks;
using CleanArchitecture.Domain.Entities;
using Microsoft.AspNetCore.Http;
using MediatR;
using CleanArchitecture.Application.Helpers;

namespace CleanArchitecture.Application.Features.Orders.Command.UpdateOrder
{
    public class UpdateOrderCommentHandler : BaseHandler, IRequestHandler<UpdateOrderCommandRequest, Unit>
    {
        private readonly OrderRules orderRules;
        public UpdateOrderCommentHandler(IUnitOfWork unitOfWork, IMapper mapper, IHttpContextAccessor httpContextAccessor, OrderRules orderRules)
            : base(unitOfWork, mapper, httpContextAccessor)
        {
            this.orderRules = orderRules;
        }

        public async Task<Unit> Handle(UpdateOrderCommandRequest request, CancellationToken cancellationToken)
        {
            LoggerHelper.LogInformation("Handling UpdateOrderRequest for orderId: {orderId}",request.Id);
            Order? oldOrder = await UnitOfWork.readRepository<Order>()
                                                .GetAsync(predicate: x => x.Id == request.Id);

            if (oldOrder == null)
            {
                LoggerHelper.LogWarning("Order with Id: {OrderId} does not exist", request.Id);
                throw new Exception("Order not found");
            }

            await orderRules.TheOrderShouldBeExist(oldOrder);

            await orderRules.TheSameUserForTheSameOrder(Guid.Parse(UserId), oldOrder.UserId);

            IList<ProductsOrders> productsOrders = await UnitOfWork.readRepository<ProductsOrders>()
                                                           .GetAllAsync(predicate: x => x.OrderId == oldOrder.Id);
            LoggerHelper.LogInformation("Deleting existing products for OrderId: {OrderId}", oldOrder.Id);

            foreach (var po in productsOrders)
            {
                LoggerHelper.LogInformation("ProductId: {ProductId} will be removed from OrderId: {OrderId}", po.ProductId, po.OrderId);
            }

            await UnitOfWork.writeRepository<ProductsOrders>().DeleteRangeAsync(productsOrders);


            Order orderObject = Mapper.Map<Order, UpdateOrderCommandRequest>(request); //new Order(Guid.Parse(UserId), orderType: Domain.Enums.OrderType.Received, TotalPrice.Sum());

            List<decimal> TotalPrice = new();

            foreach (var tempOrder in request.makeOrderDTOs)        
            {
                Product product = await UnitOfWork.readRepository<Product>()
                                                    .GetAsync(predicate: x => x.Id == tempOrder.ProductId);
                if (product == null)
                {
                    LoggerHelper.LogWarning("product with Id: {Product} not found",tempOrder.ProductId);
                    continue;
                }

                TotalPrice.Add(product.Price * tempOrder.ProductCount);
                LoggerHelper.LogInformation("ProductId: {productId} added to order with {Quantity}",tempOrder.ProductId ,tempOrder.ProductCount);

            }
            LoggerHelper.LogInformation("Total Price for Updated order : {totalPrice}", TotalPrice);

            foreach (var TempOrder in request.makeOrderDTOs)
            {
                await UnitOfWork.writeRepository<ProductsOrders>().AddAsync(new ProductsOrders()
                {
                    OrderId = orderObject.Id,
                    ProductId = TempOrder.ProductId
                });
                LoggerHelper.LogInformation("ProductId: {productId} added to OrderID: {orderID}",TempOrder.ProductId,orderObject.Id);
            }

            orderObject.UserId = oldOrder.UserId;

            orderObject.UpdatedDate = DateTime.UtcNow;

            orderObject.AddedOnDate = oldOrder.AddedOnDate;

            orderObject.TotalAmount = TotalPrice.Sum();

            await UnitOfWork.writeRepository<Order>().UpdateAsync(request.Id, orderObject);

            if (await UnitOfWork.SaveChangeAsync() > 0)
            {
                LoggerHelper.LogInformation("Order with Id: {OrderId} updated successfully", orderObject.Id);
            }
            else
            {
                LoggerHelper.LogWarning("Failed to update OrderId: {OrderId}", orderObject.Id);
            }

            return Unit.Value;
        }
    }
}
