using FluentValidation;

namespace CleanArchitecture.Application.Features.Auth.Commands.ResetPassword.ConfirmResetPassword
{
    public class ConfirmResetPasswordCommandValidator : AbstractValidator<ConfirmResetPasswordCommandRequest>
    {
        public ConfirmResetPasswordCommandValidator()
        {
            RuleFor(x => x.Email)
                .NotEmpty()
                .EmailAddress();

            RuleFor(x => x.Password)
                .NotEmpty()
                .MinimumLength(8)
;

            RuleFor(x => x.ConfirmPassword)
                .NotEmpty()
                   .WithName("Confirm Password")
                   .MinimumLength(8)
                   .Equal(prop => prop.Password);
        }
    }
}
