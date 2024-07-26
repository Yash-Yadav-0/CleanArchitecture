using CleanArchitecture.Application.Dtos;
using Microsoft.AspNetCore.Http;
using MediatR;
using Microsoft.Extensions.Configuration;
using CleanArchitecture.Application.Helpers;
using CleanArchitecture.Domain.Enums;
using System.Security.Claims;

namespace CleanArchitecture.Application.Features.Orders.Queries.GetAllOrdersForCurrentUser
{
    public class GetAllOrdersForCurrentUserQueryHandler :  
            SqlFunctionHandler<GetAllOrdersForCurrentUserQueryRequest, GetAllOrdersForCurrentUserQueryResponse>,
            IRequestHandler<GetAllOrdersForCurrentUserQueryRequest, IList<GetAllOrdersForCurrentUserQueryResponse>>
    {
        private readonly IHttpContextAccessor _httpContextAccessor;

        public GetAllOrdersForCurrentUserQueryHandler(IConfiguration configuration, IHttpContextAccessor httpContextAccessor) : base(configuration)
        {
            _httpContextAccessor = httpContextAccessor;
        }

        public async Task<IList<GetAllOrdersForCurrentUserQueryResponse>> Handle(GetAllOrdersForCurrentUserQueryRequest request, CancellationToken cancellationToken)
        {

            var userId = _httpContextAccessor.HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier);
            var functionName = "get_all_orders_for_current_user";
            var parameters = new Dictionary<string, object>
            {
                { "user_id",userId }
            };

            return await HandleAsync(
                request,
                functionName,
                reader => new GetAllOrdersForCurrentUserQueryResponse
                {
                    Id = reader.GetInt32(0),
                    TotalAmount = reader.GetDecimal(1),
                    OrderType = (OrderType)reader.GetInt16(2),
                    ProductsDTOs = reader.IsDBNull(3)
                        ? new List<ProductsDTO>()
                        : reader.GetString(3).Split(',').Select(title => new ProductsDTO { Name = title }).ToList()
                },
                cancellationToken,
                parameters
            );

        }
    }
}
