using CleanArchitecture.Application.Dtos;
using MediatR;

namespace CleanArchitecture.Application.Features.Orders.Command.UpdateOrder
{
    public class UpdateOrderCommandRequest : IRequest<Unit>
    {
        public int Id { get; set; }
        public IList<MakeOrderDTO> makeOrderDTOs { get; set; }
    }
}
