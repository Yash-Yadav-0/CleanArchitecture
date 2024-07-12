using FluentValidation;

namespace CleanArchitecture.Application.Features.Auth.Commands.RefreshToken
{
    public class RefreshTokenCommandValidator : AbstractValidator<RefreshTokenCommandRequest>
    {
        public RefreshTokenCommandValidator()
        {
            RuleFor(x => x.RefreshToken)
                .NotEmpty();

            RuleFor(x => x.AccessToken)
                .NotEmpty();
        }
    }
}
