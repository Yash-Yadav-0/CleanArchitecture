using CleanArchitecture.Application.Dtos;
using MediatR;

namespace CleanArchitecture.Application.Features.Orders.Comments.MakeOrder
{
    public class MakeOrderCommandRequest : IRequest<Unit>
    {
        public IList<MakeOrderDTO> makeOrderDTOs { get; set; }
    }
}
