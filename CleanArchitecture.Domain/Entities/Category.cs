using CleanArchitecture.Domain.Common;

namespace CleanArchitecture.Domain.Entities
{
    public class Category : EntityBase
    {
        public Category(int parentId, string name, int priorty)
        {
            ParentId = parentId;
            Name = name;
            Priorty = priorty;
        }
        public Category() { }
        public int ParentId { get; set; }
        public string Name { get; set; }
        public int Priorty { get; set; }
        public virtual ICollection<Details> Details { get; set; }
        public virtual ICollection<ProductsCategories> ProductsCategory { get; set; }
    }
}
