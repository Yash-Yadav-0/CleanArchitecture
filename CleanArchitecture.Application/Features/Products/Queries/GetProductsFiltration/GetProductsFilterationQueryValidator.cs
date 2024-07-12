using FluentValidation;

namespace CleanArchitecture.Application.Features.Products.Queries.GetProductsFiltration
{
    public class GetProductsFilterationQueryValidator : AbstractValidator<GetProductsFilterationQueryRequest>
    {
        public GetProductsFilterationQueryValidator()
        {
            RuleFor(x => x.SearchInput)
                .NotEmpty()
                .NotNull();
        }
    }
}
