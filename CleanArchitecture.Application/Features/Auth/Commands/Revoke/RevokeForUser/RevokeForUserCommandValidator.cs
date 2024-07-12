using FluentValidation;

namespace CleanArchitecture.Application.Features.Auth.Commands.Revoke.RevokeForUser
{
    public class RevokeForUserCommandValidator : AbstractValidator<RevokeForUserCommandRequest>
    {
        public RevokeForUserCommandValidator()
        {
            RuleFor(x => x.Email)
                .EmailAddress()
                .NotEmpty();
        }
    }
}
