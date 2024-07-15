using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using CleanArchitecture.Domain.Entities;
using Bogus;

namespace CleanArchitecture.Persistence.Configuration
{
    public class DetailConfiguration : IEntityTypeConfiguration<Details>
    {
        public void Configure(EntityTypeBuilder<Details> builder)
        {
            Faker faker = new("en");

            Details detail01 = new()
            {
                Id = 1,
                Title = faker.Lorem.Sentence(1),
                Description = faker.Lorem.Sentence(5),
                CategoryId = 1,
                AddedOnDate = DateTime.UtcNow,
                IsDeleted = false,
            };
            Details detail02 = new()
            {
                Id = 2,
                Title = faker.Lorem.Sentence(2),
                Description = faker.Lorem.Sentence(5),
                CategoryId = 3,
                AddedOnDate = DateTime.UtcNow,
                IsDeleted = true,
            };
            Details detail03 = new()
            {
                Id = 3,
                Title = faker.Lorem.Sentence(1),
                Description = faker.Lorem.Sentence(5),
                CategoryId = 4,
                AddedOnDate = DateTime.UtcNow,
                IsDeleted = false,
            };

            builder.HasData(detail01, detail02, detail03);
        }
    }
}
