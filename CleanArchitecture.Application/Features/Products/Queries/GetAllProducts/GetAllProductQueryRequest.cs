using CleanArchitecture.Application.Interfaces.RedisCache;
using MediatR;

namespace CleanArchitecture.Application.Features.Products.Queries.GetAllProducts
{
    public class GetAllProductQueryRequest : IRequest<IList<GetAllProductQueryResponse>>, ICacheableQuery
    {
        public string CacheKey => "GetAllProducts";
        public double CacheTime => 6;
    }
}
