using CleanArchitecture.Application.Bases;
using CleanArchitecture.Application.Features.Orders.Rules;
using CleanArchitecture.Application.Helpers;
using CleanArchitecture.Application.Interfaces.AutoMapper;
using CleanArchitecture.Application.Interfaces.UnitOfWorks;
using CleanArchitecture.Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Http;

namespace CleanArchitecture.Application.Features.Orders.Comments.DeleteOrder
{
    public class DeleteOrderCommendHandler : BaseHandler, IRequestHandler<DeleteOrderCommandRequest, Unit>
    {
        private readonly OrderRules orderRules;

        public DeleteOrderCommendHandler(IUnitOfWork unitOfWork, IMapper mapper, IHttpContextAccessor httpContextAccessor, OrderRules orderRules)
            : base(unitOfWork, mapper, httpContextAccessor)
        {
            this.orderRules = orderRules;
        }
        public async Task<Unit> Handle(DeleteOrderCommandRequest request, CancellationToken cancellationToken)
        {
            LoggerHelper.LogInformation("Handling DeleteOrderCommand for OrderId: {OrderId}",request.Id);
            var order = await UnitOfWork.readRepository<Order>().GetAsync(predicate: x => x.Id == request.Id);

            if (order == null)
            {
                LoggerHelper.LogWarning("Order with Id: {OrderId} not found", request.Id);
                await orderRules.TheOrderShouldBeExist(null);
            }
            else
            {
                await orderRules.TheOrderShouldBeExist(order);
                await orderRules.TheSameUserForTheSameOrder(order.UserId, Guid.Parse(UserId));

                order.IsDeleted = true;
                order.DeletedDate = DateTime.UtcNow;

                await UnitOfWork.writeRepository<Order>().UpdateAsync(request.Id, order);
                await UnitOfWork.SaveChangeAsync();
                LoggerHelper.LogInformation("Order with IdL {OrderId} marked as deleted successfully", request.Id);
            }
            return Unit.Value;
        }
    }
}
