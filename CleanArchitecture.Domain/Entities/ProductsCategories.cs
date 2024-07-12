using CleanArchitecture.Domain.Common;
using System.ComponentModel.DataAnnotations.Schema;


namespace CleanArchitecture.Domain.Entities
{
    public class ProductsCategories : IEntityBase
    {
        [ForeignKey("Product")]
        public int ProductId { get; set; }
        public virtual Product Product { get; set; }

        [ForeignKey("Category")]
        public int CategoryId { get; set; }
        public virtual Category Category { get; set; }
    }
}
