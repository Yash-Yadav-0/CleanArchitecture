using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using Bogus;
using CleanArchitecture.Domain.Entities;

namespace CleanArchitecture.Persistence.Configuration
{
    public class CategoryConfiguration : IEntityTypeConfiguration<Category>
    {
        public void Configure(EntityTypeBuilder<Category> builder)
        {
            Faker faker = new("en");

            Category category01 = new()
            {
                Id = 1,
                Name = "Electric",
                Priorty = 1,
                ParentId = 0,
                IsDeleted = false,
                AddedOnDate = DateTime.UtcNow,
            };

            Category category02 = new()
            {
                Id = 2,
                Name = "ElModa",
                Priorty = 2,
                ParentId = 0,
                IsDeleted = false,
                AddedOnDate = DateTime.UtcNow,
            };

            Category parent01 = new()
            {
                Id = 3,
                Name = "Computer",
                Priorty = 1,
                ParentId = 1,
                IsDeleted = false,
                AddedOnDate = DateTime.UtcNow,
            };

            Category parent02 = new()
            {
                Id = 4,
                Name = "Women",
                Priorty = 1,
                ParentId = 2,
                IsDeleted = false,
                AddedOnDate = DateTime.UtcNow,
            };
            builder.HasData(category01, category02, parent01, parent02);
        }
    }
}
