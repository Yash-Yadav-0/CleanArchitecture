using CleanArchitecture.Domain.Entities;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using Bogus;

namespace CleanArchitecture.Persistence.Configuration
{
    public class BrandConfiguration : IEntityTypeConfiguration<Brand>
    {
        public void Configure(EntityTypeBuilder<Brand> builder)
        {
            Faker faker = new("en");
            Brand brand01 = new Brand()
            {
                Id = 1,
                Name = faker.Commerce.Department(),
                AddedOnDate = DateTime.UtcNow,
                IsDeleted = false
            };

            Brand brand02 = new Brand()
            {
                Id = 2,
                Name = faker.Commerce.Department(),
                AddedOnDate = DateTime.UtcNow,
                IsDeleted = true
            };

            Brand brand03 = new Brand()
            {
                Id = 3,
                Name = faker.Commerce.Department(),
                AddedOnDate = DateTime.UtcNow,
                IsDeleted = false
            };

            builder.HasData(brand01, brand02, brand03);
        }
    }
}
