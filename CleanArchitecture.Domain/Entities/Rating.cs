using CleanArchitecture.Domain.Common;
using System.ComponentModel.DataAnnotations.Schema;

namespace CleanArchitecture.Domain.Entities
{
    public class Rating : IEntityBase
    {
        public Guid RatingId { get; set; }
        public int Rate { get; set; }
        public int LikeCount { get; set; }
        public int DislikeCount { get; set; }
        public string Comment { get; set; }

        [ForeignKey("User")]
        public Guid UserId { get; set; }
        public virtual User User { get; set; }

        [ForeignKey("Product")]
        public int ProductId { get; set; }
        public virtual Product Product { get; set; }
    }
}
