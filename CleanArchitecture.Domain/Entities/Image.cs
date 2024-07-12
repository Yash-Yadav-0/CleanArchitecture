using CleanArchitecture.Domain.Common;

namespace CleanArchitecture.Domain.Entities
{
    public class Image : EntityBase
    {
        public string FileName { get; set; }
        public string Path { get; set; }
        public int ProductId { get; set; }
        public virtual Product Product { get; set; }
    }
}
