using CleanArchitecture.Application.Interfaces.RedisCache;
using MediatR;

namespace CleanArchitecture.Application.Features.Orders.Queries.GetAllOrders
{
    public class GetAllOrdersQueryRequest : IRequest<IList<GetAllOrdersQueryResponse>>, ICacheableQuery
    {
        public string CacheKey => "GetAllOrders";
        public double CacheTime => 6;
    }
}
