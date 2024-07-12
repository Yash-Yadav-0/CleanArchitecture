using FluentValidation;

namespace CleanArchitecture.Application.Features.Auth.Commands.EmailConfirmation
{
    public class EmailConfirmationCommandValidator : AbstractValidator<EmailConfirmationCommandRequest>
    {
        public EmailConfirmationCommandValidator()
        {
            RuleFor(x => x.Email)
                .NotNull()
                .EmailAddress();

            RuleFor(x => x.Token)
                .NotNull();
        }
    }
}
