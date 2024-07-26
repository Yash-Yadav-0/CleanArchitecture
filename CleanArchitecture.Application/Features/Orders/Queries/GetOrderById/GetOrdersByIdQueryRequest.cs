using MediatR;

namespace CleanArchitecture.Application.Features.Orders.Queries.GetOrderById
{
    public class GetOrderByIdQueryRequest : IRequest<IList<GetOrderByIdQueryResponse>>  //, ICacheableQuery
    {
        public int OrderId { get; set; }
    }
}
