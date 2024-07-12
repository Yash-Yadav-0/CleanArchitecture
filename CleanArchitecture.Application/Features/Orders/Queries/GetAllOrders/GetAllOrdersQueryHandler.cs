using CleanArchitecture.Application.Bases;
using CleanArchitecture.Application.Dtos;
using CleanArchitecture.Application.Interfaces.AutoMapper;
using CleanArchitecture.Application.Interfaces.UnitOfWorks;
using CleanArchitecture.Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Http;

namespace CleanArchitecture.Application.Features.Orders.Queries.GetAllOrders
{
    public class GetAllOrdersQueryHandler : BaseHandler, IRequestHandler<GetAllOrdersQueryRequest, IList<GetAllOrdersQueryResponse>>
    {
        public GetAllOrdersQueryHandler(IUnitOfWork unitOfWork, IMapper mapper, IHttpContextAccessor httpContextAccessor)
            : base(unitOfWork, mapper, httpContextAccessor)
        {
        }
        public async Task<IList<GetAllOrdersQueryResponse>> Handle(GetAllOrdersQueryRequest request, CancellationToken cancellationToken)
        {
            var orders = await UnitOfWork.readRepository<Order>().GetAllAsync();
            //  var products=await UnitOfWork.readRepository<Product>().GetAllAsync();

            var queryResponse = Mapper.Map<GetAllOrdersQueryResponse, Order>(orders);

            foreach (var orderobj in queryResponse)
            {
                var productsTitle = from order in orders
                                    join Orderproducts in orders.SelectMany(x => x.ProductsOrders)
                                    on order.Id equals Orderproducts.OrderId into
                                    demo
                                    select new
                                    {
                                        productsNames = from Orderproducts in demo
                                                        join products in order.ProductsOrders.Select(x => x.product)
                                                        on Orderproducts.ProductId equals products.Id
                                        where Orderproducts.OrderId == orderobj.Id
                                                        select new { products.Title }
                                    };

                var titles = productsTitle.SelectMany(x => x.productsNames.Select(x => x.Title)).ToList();

                orderobj.ProductsDTOs = titles.Select(x => new ProductsDTO() { Name = x }).ToList();

            }
            return queryResponse;
        }
    }
}
