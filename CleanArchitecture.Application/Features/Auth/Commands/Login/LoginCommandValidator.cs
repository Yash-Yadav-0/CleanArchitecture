using FluentValidation;

namespace CleanArchitecture.Application.Features.Auth.Commands.Login
{
    public class LoginCommandValidator : AbstractValidator<LoginCommandRequest>
    {
        public LoginCommandValidator()
        {
            RuleFor(prop => prop.Email)
                .EmailAddress()
                .NotEmpty();

            RuleFor(prop => prop.Password)
                .NotEmpty();
        }
    }
}
