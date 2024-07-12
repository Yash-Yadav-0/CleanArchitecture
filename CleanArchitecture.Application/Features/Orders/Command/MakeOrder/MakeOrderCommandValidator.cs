using FluentValidation;

namespace CleanArchitecture.Application.Features.Orders.Comments.MakeOrder
{
    public class MakeOrderCommandValidator : AbstractValidator<MakeOrderCommandRequest>
    {
        public MakeOrderCommandValidator()
        {
        }
    }
}
