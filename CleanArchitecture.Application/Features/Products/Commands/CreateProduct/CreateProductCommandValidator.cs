﻿using FluentValidation;

namespace CleanArchitecture.Application.Features.Products.Commands.CreateProduct
{
    public class CreateProductCommandValidator : AbstractValidator<CreateProductCommandRequest>
    {
        public CreateProductCommandValidator()
        {
            RuleFor(prop => prop.Title).NotEmpty()
                                       .WithName("Name Of Product");

            RuleFor(prop => prop.Description).NotEmpty()
                                             .WithName("Description Of Product");

            RuleFor(prop => prop.Price).NotEmpty()
                                       .GreaterThan(0);

            RuleFor(prop => prop.Discount).NotEmpty()
                                          .GreaterThanOrEqualTo(0);


            RuleFor(prop => prop.BrandId).NotEmpty()
                                          .GreaterThan(0)
                                          .WithName("Number of Brand");

            RuleFor(prop => prop.CategortIds).NotEmpty()
                                          .Must(category => category.Any())
                                          .WithName("Categories");
        }
    }
}
