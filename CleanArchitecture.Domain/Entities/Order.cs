using CleanArchitecture.Domain.Common;
using CleanArchitecture.Domain.Enums;
using System.ComponentModel.DataAnnotations.Schema;

namespace CleanArchitecture.Domain.Entities
{
    public class Order : EntityBase
    {
        public Order()
        {
            this.ProductsOrders = new HashSet<ProductsOrders>();
        }
        public Order(Guid userId, OrderType orderType, decimal totalAmount)
        {
            UserId = userId;
            OrderType = orderType;
            TotalAmount = totalAmount;
            this.ProductsOrders = new HashSet<ProductsOrders>();
        }
        public Decimal TotalAmount { get; set; }
        public OrderType OrderType { get; set; }
        [ForeignKey("user")]
        public Guid UserId { get; set; }
        public virtual User user { get; set; }
        public virtual ICollection<ProductsOrders> ProductsOrders { get; set; }
    }
}
