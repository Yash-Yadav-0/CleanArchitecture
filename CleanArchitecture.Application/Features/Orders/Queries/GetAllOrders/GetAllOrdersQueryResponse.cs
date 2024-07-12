using CleanArchitecture.Application.Dtos;
using CleanArchitecture.Domain.Enums;

namespace CleanArchitecture.Application.Features.Orders.Queries.GetAllOrders
{
    public class GetAllOrdersQueryResponse
    {
        public int Id { get; set; }
        public Decimal TotalAmount { get; set; }
        public OrderType OrderType { get; set; }
        public Guid UserId { get; set; }
        public IList<ProductsDTO> ProductsDTOs { get; set; }
    }
}
