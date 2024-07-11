using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanArchitecture.Application.Features.Orders.Comments.MakeOrder
{
    public class MakeOrderCommandValidator : AbstractValidator<MakeOrderCommandRequest>
    {
        public MakeOrderCommandValidator() { }
    }
}
