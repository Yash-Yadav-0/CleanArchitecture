using FluentValidation;

namespace CleanArchitecture.Application.Features.Auth.Commands.ResetPassword.SendForResetPassword
{
    public class SendForResetPasswordCommandsValidator : AbstractValidator<SendForResetPasswordCommandsRequest>
    {
        public SendForResetPasswordCommandsValidator()
        {
            RuleFor(x => x.Email)
                .NotNull()
                .NotEmpty()
                .EmailAddress();
        }
    }
}
