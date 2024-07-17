using FluentValidation;

namespace CleanArchitecture.Application.Features.UserFeature.Commands.ChangeToMember
{
    public class ChangeToMemberCommandValidator : AbstractValidator<ChangeToMemberCommandRequest>
    {
        public ChangeToMemberCommandValidator()
        {
            RuleFor(prop => prop.Email)
                .EmailAddress()
                .NotEmpty();
        }
    }
}
