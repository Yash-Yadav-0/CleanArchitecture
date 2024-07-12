using CleanArchitecture.Application.Bases;
using CleanArchitecture.Application.Interfaces.AutoMapper;
using CleanArchitecture.Application.Interfaces.UnitOfWorks;
using CleanArchitecture.Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Http;

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
            List<decimal> TotalPrice = new();

            foreach (var order in request.makeOrderDTOs)
            {
                Product product = await UnitOfWork.readRepository<Product>().GetAsync(predicate: x => x.Id == order.ProductId);

                TotalPrice.Add(product.Price * order.ProductCount);

            }
            Guid UserIdFromToken = Guid.Parse(UserId);

            Order orderObject = new Order(UserIdFromToken, orderType: Domain.Enums.OrderType.Received, TotalPrice.Sum());

            await UnitOfWork.writeRepository<Order>().AddAsync(orderObject);

            if (await UnitOfWork.SaveChangeAsync() > 0)
            {
                foreach (var order in request.makeOrderDTOs)
                {
                    await UnitOfWork.writeRepository<ProductsOrders>().AddAsync(new ProductsOrders()
                    {
                        OrderId = orderObject.Id,

                        ProductId = order.ProductId
                    });
                }
            }
            await UnitOfWork.SaveChangeAsync();

            return Unit.Value;
        }
    }
}
