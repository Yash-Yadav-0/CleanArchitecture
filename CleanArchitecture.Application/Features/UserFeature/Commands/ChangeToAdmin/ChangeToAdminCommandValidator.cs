using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanArchitecture.Application.Features.UserFeature.Commands.ChangeToAdmin
{
    public class ChangeToAdminCommandValidator :AbstractValidator<ChangeToAdminCommandRequest>
    {
        public ChangeToAdminCommandValidator()
        {
            RuleFor(prop => prop.Email)
                .EmailAddress()
                .NotEmpty();
        }
    }
}
