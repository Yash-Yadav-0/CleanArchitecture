using CleanArchitecture.Application.Bases;
using CleanArchitecture.Application.Dtos;
using CleanArchitecture.Application.Helpers;
using CleanArchitecture.Application.Interfaces.AutoMapper;
using CleanArchitecture.Application.Interfaces.UnitOfWorks;
using CleanArchitecture.Domain.Entities;
using CleanArchitecture.Domain.Enums;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;

namespace CleanArchitecture.Application.Features.Orders.Queries.GetAllOrders
{
    public class GetAllOrdersQueryHandler : SqlFunctionHandler<GetAllOrdersQueryRequest, GetAllOrdersQueryResponse>, IRequestHandler<GetAllOrdersQueryRequest, IList<GetAllOrdersQueryResponse>>
    {
        public GetAllOrdersQueryHandler(IConfiguration configuration) : base(configuration)
        {
        }

        public async Task<IList<GetAllOrdersQueryResponse>> Handle(GetAllOrdersQueryRequest request, CancellationToken cancellationToken)
        {
            LoggerHelper.LogInformation("Handling GetAllOrdersQueryRequest");

            IList<GetAllOrdersQueryResponse> orders;
            try
            {
                orders = await HandleAsync(
                    request,
                    "get_all_orders",
                    reader => new GetAllOrdersQueryResponse
                    {
                        Id = reader.GetInt32(0),
                        TotalAmount = reader.GetDecimal(1),
                        OrderType = (OrderType)reader.GetInt32(2),
                        UserId = reader.GetGuid(3)
                    },
                    cancellationToken
                );

                LoggerHelper.LogInformation("Successfully retrieved {OrderCount} orders", orders.Count);
            }
            catch (Exception ex)
            {
                LoggerHelper.LogError("Error occurred while handling GetAllOrdersQueryRequest", ex);
                throw; // Re-throw the exception after logging it
            }

            return orders;
        }
    }
}
