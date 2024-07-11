using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
