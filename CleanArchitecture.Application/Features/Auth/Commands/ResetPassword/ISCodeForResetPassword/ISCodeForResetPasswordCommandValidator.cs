using FluentValidation;

namespace CleanArchitecture.Application.Features.Auth.Commands.ResetPassword.ISCodeForResetPassword
{
    public class ISCodeForResetPasswordCommandValidator : AbstractValidator<ISCodeForResetPasswordCommandRequest>
    {
        public ISCodeForResetPasswordCommandValidator()
        {
            RuleFor(x => x.Email)
                .NotEmpty()
                .NotNull()
                .EmailAddress();

            RuleFor(x => x.CodeOfConfirm)
                .NotEmpty();
        }
    }
}
