using FluentValidation;

namespace CleanArchitecture.Application.Features.Orders.Comments.DeleteOrder
{
    public class DeleteOrderCommandValidator : AbstractValidator<DeleteOrderCommandRequest>
    {
        public DeleteOrderCommandValidator()
        {
            RuleFor(x => x.Id)
                .NotEmpty()
                .GreaterThan(0);
        }
    }
}
