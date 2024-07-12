using FluentValidation;

namespace CleanArchitecture.Application.Features.Orders.Queries.GetOrderById
{
    public class GetOrderByIdQueryValidator : AbstractValidator<GetOrderByIdQueryRequest>
    {
        public GetOrderByIdQueryValidator()
        {
            RuleFor(x => x.OrderId)
                .NotEmpty()
                .NotNull()
                .GreaterThan(0);
        }
    }
}
