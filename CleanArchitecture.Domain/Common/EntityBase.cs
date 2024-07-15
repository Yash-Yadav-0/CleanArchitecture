
namespace CleanArchitecture.Domain.Common
{
    public class EntityBase : IEntityBase
    {
        public int Id { get; set; }
        public DateTime AddedOnDate { get; set; } = DateTime.UtcNow;
        public DateTime UpdatedDate { get; set; } = DateTime.UtcNow;
        public virtual DateTime? DeletedDate {  get; set; } = DateTime.UtcNow;
        public bool IsDeleted { get; set; }
    }
}
