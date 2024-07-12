using CleanArchitecture.Application.Dtos;
using CleanArchitecture.Domain.Enums;

namespace CleanArchitecture.Application.Features.Orders.Queries.GetAllOrdersForCurrentUser
{
    public class GetAllOrdersForCurrentUserQueryResponse
    {
        public int Id { get; set; }
        public Decimal TotalAmount { get; set; }
        public OrderType OrderType { get; set; }
        public IList<ProductsDTO> ProductsDTOs { get; set; }
    }
}
