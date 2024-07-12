using CleanArchitecture.Domain.Entities;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;

namespace CleanArchitecture.Persistence.Configuration
{
    internal class RoleConfiguration : IEntityTypeConfiguration<Role>
    {
        public void Configure(EntityTypeBuilder<Role> builder)
        {
            Role Admin = new Role()
            {
                Id = Guid.NewGuid(),
                Name = "Admin",
                NormalizedName = "ADMIN",
                ConcurrencyStamp = Guid.NewGuid().ToString()
            };
            Role USER = new Role()
            {
                Id = Guid.NewGuid(),
                Name = "User",
                NormalizedName = "USER",
                ConcurrencyStamp = Guid.NewGuid().ToString()
            };
            Role Vendor = new Role()
            {
                Id = Guid.NewGuid(),
                Name = "Vendor",
                NormalizedName = "VENDOR",
                ConcurrencyStamp = Guid.NewGuid().ToString()
            };
            builder.HasData(Admin, USER);
        }
    }
}
