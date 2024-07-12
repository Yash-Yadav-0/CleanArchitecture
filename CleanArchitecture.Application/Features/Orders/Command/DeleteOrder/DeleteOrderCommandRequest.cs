using MediatR;

namespace CleanArchitecture.Application.Features.Orders.Comments.DeleteOrder
{
    public class DeleteOrderCommandRequest : IRequest<Unit>
    {
        public int Id { get; set; }
    }
}
