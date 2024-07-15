using CleanArchitecture.Application.Bases;
using CleanArchitecture.Application.Features.Orders.Rules;
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
            var order = await UnitOfWork.readRepository<Order>().GetAsync(predicate: x => x.Id == request.Id);

            await orderRules.TheOrderShouldBeExist(order);
            await orderRules.TheSameUserForTheSameOrder(order.UserId, Guid.Parse(UserId));

            order.IsDeleted = true;
            order.DeletedDate = DateTime.UtcNow;

            await UnitOfWork.writeRepository<Order>().UpdateAsync(request.Id, order);
            await UnitOfWork.SaveChangeAsync();

            return Unit.Value;
        }
    }
}
