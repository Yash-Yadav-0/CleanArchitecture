using CleanArchitecture.Application.Interfaces.RedisCache;
using MediatR;


namespace CleanArchitecture.Application.Features.Orders.Queries.GetAllOrdersForCurrentUser
{
    public class GetAllOrdersForCurrentUserQueryRequest : ICacheableQuery, IRequest<IList<GetAllOrdersForCurrentUserQueryResponse>>
    {
        //[DefaultValue("7164AB9D-DE00-41A5-3FF0-08DC86B45C88")]
        //public Guid UserId { get; set; }
        public string CacheKey => "GetAllOrdersForCurrentUser";
        public double CacheTime => 3;
    }
}
