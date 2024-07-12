using CleanArchitecture.Domain.Common;

namespace CleanArchitecture.Domain.Entities
{
    public class Brand : EntityBase
    {
        public string Name { get; set; }
        public string? Picture { get; set; }
        public virtual ICollection<Product> products { get; set; }
        public Brand() { }
        public Brand(string name, string? picture)
        {
            Name = name;
            Picture = picture;
        }
    }
}
