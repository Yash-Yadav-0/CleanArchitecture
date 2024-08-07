﻿using CleanArchitecture.Domain.Entities;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using Bogus;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanArchitecture.Persistence.Configuration
{
    public class ProductConfiguration : IEntityTypeConfiguration<Product>
    {
        public void Configure(EntityTypeBuilder<Product> builder)
        {
            Faker faker = new("en");

            Product product01 = new Product()
            {
                Id = 1,
                Title = faker.Commerce.ProductName(),
                Description = faker.Commerce.ProductDescription(),
                Price = faker.Finance.Amount(50, 2000),
                Discount = faker.Random.Decimal(10, 50),
                AddedOnDate = DateTime.UtcNow,
                IsDeleted = false,
                BrandId = 1
            };
            Product product02 = new Product()
            {
                Id = 2,
                Title = faker.Commerce.ProductName(),
                Description = faker.Commerce.ProductDescription(),
                Price = faker.Finance.Amount(50, 2000),
                Discount = faker.Random.Decimal(10, 50),
                AddedOnDate = DateTime.UtcNow,
                IsDeleted = false,
                BrandId = 1
            };
            Product product03 = new Product()
            {
                Id = 3,
                Title = faker.Commerce.ProductName(),
                Description = faker.Commerce.ProductDescription(),
                Price = faker.Finance.Amount(50, 2000),
                Discount = faker.Random.Decimal(10, 50),
                AddedOnDate = DateTime.UtcNow,
                IsDeleted = true,
                BrandId = 3
            };

            builder.HasData(product01, product02, product03);
        }
    }
}
