
namespace CleanArchitecture.Domain.Common
{
    public class EntityBase : IEntityBase
    {
        public int Id { get; set; }
        public DateTime AddedOnDate { get; set; } = DateTime.Now;
        public DateTime UpdatedDate { get; set; } = DateTime.Now;
        public virtual DateTime? DeletedDate {  get; set; } = DateTime.Now;
        public bool IsDeleted { get; set; }
    }
}
