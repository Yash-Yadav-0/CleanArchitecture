using CleanArchitecture.Application.Bases;
using CleanArchitecture.Application.Dtos;
using CleanArchitecture.Application.Features.Orders.Queries.GetAllOrdersForCurrentUser;
using CleanArchitecture.Application.Helpers;
using CleanArchitecture.Domain.Enums;
using MediatR;
using Microsoft.Extensions.Configuration;

namespace CleanArchitecture.Application.Features.Orders.Queries.GetOrderById
{
    public class GetOrderByIdQueryHandler : 
            SqlFunctionHandler<GetOrderByIdQueryRequest,GetOrderByIdQueryResponse>, 
            IRequestHandler<GetOrderByIdQueryRequest, IList<GetOrderByIdQueryResponse>>
    {
        private readonly IConfiguration configuration;
        public GetOrderByIdQueryHandler(IConfiguration configuration)
            : base(configuration)
        {
        }
        public async Task<IList<GetOrderByIdQueryResponse>> Handle(GetOrderByIdQueryRequest request, CancellationToken cancellationToken)
        {
            LoggerHelper.LogInformation("Handling GetOrderByIdQueryRequest for OrderId: {OrderId}", request.OrderId);

            var functionName = "get_order_by_id";
            var parameters = new Dictionary<string, object>
            {
                { "order_id", request.OrderId }
            };

            try
            {
                var orders = await HandleAsync(
                    request,
                    functionName,
                    reader => new GetOrderByIdQueryResponse
                    {
                        Id = reader.GetInt32(0),
                        TotalAmount = reader.GetDecimal(1),
                        OrderType = (OrderType)reader.GetInt16(2),
                        UserId = reader.GetGuid(3),
                        ProductsDTOs = reader.IsDBNull(4)
                            ? new List<ProductsDTO>()
                            : reader.GetString(4).Split(',').Select(title => new ProductsDTO { Name = title }).ToList()
                    },
                    cancellationToken,
                    parameters
                );

                LoggerHelper.LogInformation("Successfully retrieved {OrderCount} orders for OrderId: {OrderId}", orders.Count, request.OrderId);

                return orders;
            }
            catch (Exception ex)
            {
                LoggerHelper.LogError("Error occurred while handling GetOrderByIdQueryRequest for OrderId: {OrderId}",ex, request.OrderId);
                throw; // Re-throw the exception after logging it
            }
        }
    }
}
