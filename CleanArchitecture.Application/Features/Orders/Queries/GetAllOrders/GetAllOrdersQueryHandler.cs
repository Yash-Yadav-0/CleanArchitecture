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
            var functionName = "get_all_orders";
            return await HandleAsync(
                request,
                functionName,
                reader => new GetAllOrdersQueryResponse
                {
                    Id = reader.GetInt32(0),
                    TotalAmount = reader.GetDecimal(1),
                    OrderType = (OrderType)reader.GetInt32(2),
                    UserId = reader.GetGuid(3)
                },
                cancellationToken
            );
        }
    }
}
