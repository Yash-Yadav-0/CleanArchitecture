using CleanArchitecture.Application.Interfaces.RedisCache;
using MediatR;

namespace CleanArchitecture.Application.Features.Products.Queries.GetProductById
{
    public class GetProductByIdQueryRequest : IRequest<GetProductByIdQueryResponse>, ICacheableQuery
    {
        public int ProductId { get; set; }
        public string CacheKey => "GetProductById";
        public double CacheTime => 6;
    }
}
