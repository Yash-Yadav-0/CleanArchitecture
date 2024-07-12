using FluentValidation;

namespace CleanArchitecture.Application.Features.Orders.Command.UpdateOrder
{
    public class UpdateOrderCommandValidator : AbstractValidator<UpdateOrderCommandRequest>
    {
        public UpdateOrderCommandValidator()
        {
            //RuleFor(x => x.makeOrderDTOs.Select(x => x.ProductId))
            //    .NotNull()
            //    .NotEmpty();


            //RuleFor(x => x.makeOrderDTOs.Select(x=>x.ProductCount))
            //    .NotEmpty()
            //    .NotEmpty()
            //    .Custom(x=>x.);
        }
    }
}
