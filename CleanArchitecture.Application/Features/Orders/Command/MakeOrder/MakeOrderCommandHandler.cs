using CleanArchitecture.Application.Bases;
using CleanArchitecture.Application.Helpers;
using CleanArchitecture.Application.Interfaces.AutoMapper;
using CleanArchitecture.Application.Interfaces.UnitOfWorks;
using CleanArchitecture.Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.IdentityModel.Logging;

namespace CleanArchitecture.Application.Features.Orders.Comments.MakeOrder
{
    public class MakeOrderCommandHandler : BaseHandler, IRequestHandler<MakeOrderCommandRequest, Unit>
    {
        public MakeOrderCommandHandler(IUnitOfWork unitOfWork, IMapper mapper, IHttpContextAccessor httpContextAccessor)
            : base(unitOfWork, mapper, httpContextAccessor)
        {
        }
        public async Task<Unit> Handle(MakeOrderCommandRequest request, CancellationToken cancellationToken)
        {
            LogHelper.LogInformation("Handling MakeOrderCOmmandRequest for UserId:{UserID}", Guid.Parse(UserId));
            List<decimal> totalPrice = new();

            foreach (var order in request.makeOrderDTOs)
            {
                Product product = await UnitOfWork.readRepository<Product>().GetAsync(predicate: x => x.Id == order.ProductId);
                if(product == null)
                {
                    LoggerHelper.LogWarning("Product with Id: {ProductId} not found", order.ProductId);
                    continue;
                }

                totalPrice.Add(product.Price * order.ProductCount);
                LoggerHelper.LogInformation("Product found: {ProductId}, Price: {Price}, Quantity: {Quantity}", order.ProductId, product.Price, order.ProductCount);
            }
            if (!totalPrice.Any())
            {
                LoggerHelper.LogWarning("No valid products found in the order request.");
                return Unit.Value;
            }

            Guid UserIdFromToken = Guid.Parse(UserId);

            Order orderObject = new Order(UserIdFromToken, orderType: Domain.Enums.OrderType.Received, totalPrice.Sum());

            await UnitOfWork.writeRepository<Order>().AddAsync(orderObject);

            if (await UnitOfWork.SaveChangeAsync() > 0)
            {
                foreach (var order in request.makeOrderDTOs)
                {
                    await UnitOfWork.writeRepository<ProductsOrders>().AddAsync(new ProductsOrders
                    {
                        OrderId = orderObject.Id,
                        ProductId = order.ProductId
                    });
                }

                await UnitOfWork.SaveChangeAsync();
                LoggerHelper.LogInformation("Order successfully created with Id: {OrderId}", orderObject.Id);
            }
            else
            {
                LoggerHelper.LogWarning("Failed to save changes for the new order.");
            }

            return Unit.Value;
        }
    }
}
