using Microsoft.AspNetCore.Identity;
using System.Security;

namespace CleanArchitecture.Domain.Entities
{
    public class Role : IdentityRole<Guid>
    {
        public Permissions Permissions { get; set; }
        public DateTime? CreatedAt { get; set; }
        public string? CreatedBy { get; set; }
        public DateTime? LastModifiedAt { get; set; }
        public string? LastModifiedBy { get; set; }
    }
}
