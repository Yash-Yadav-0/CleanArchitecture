using CleanArchitecture.Application.Bases;
using CleanArchitecture.Application.Dtos;
using CleanArchitecture.Application.Interfaces.AutoMapper;
using CleanArchitecture.Application.Interfaces.UnitOfWorks;
using CleanArchitecture.Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Http;

namespace CleanArchitecture.Application.Features.Orders.Queries.GetOrderById
{
    public class GetOrderByIdQueryHandler : BaseHandler, IRequestHandler<GetOrderByIdQueryRequest, GetOrderByIdQueryResponse>
    {
        public GetOrderByIdQueryHandler(IUnitOfWork unitOfWork, IMapper mapper, IHttpContextAccessor httpContextAccessor)
            : base(unitOfWork, mapper, httpContextAccessor)
        {
        }
        public async Task<GetOrderByIdQueryResponse> Handle(GetOrderByIdQueryRequest request, CancellationToken cancellationToken)
        {
            var order = await UnitOfWork.readRepository<Order>().GetAsync(predicate: x => x.Id == request.OrderId);

            var queryResponse = Mapper.Map<GetOrderByIdQueryResponse, Order>(order);

            var productsData = (from ProductsOrders in order.ProductsOrders
            join products in order.ProductsOrders.Select(x => x.product)
                                on ProductsOrders.OrderId equals products.Id
                                where ProductsOrders.OrderId == order.Id
                                select products);

            queryResponse.ProductsDTOs = productsData.Select(x => new ProductsDTO() { Name = x.Title }).ToList();
            return queryResponse;
        }
    }
}
