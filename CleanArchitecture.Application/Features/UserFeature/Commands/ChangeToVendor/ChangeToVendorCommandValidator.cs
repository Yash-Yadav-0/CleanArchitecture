using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanArchitecture.Application.Features.UserFeature.Commands.ChangeToVendor
{
    public class ChangeToVendorCommandValidator : AbstractValidator<ChangeToVendorCommandRequest>
    {
        public ChangeToVendorCommandValidator()
        {
            RuleFor(prop => prop.Email)
                .EmailAddress()
                .NotEmpty();
        }
    }
}
