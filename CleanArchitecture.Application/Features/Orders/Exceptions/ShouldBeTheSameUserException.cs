using CleanArchitecture.Application.Bases;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanArchitecture.Application.Features.Orders.Exceptions
{
    public class TheSameUserForTheSameOrderException : BaseException
    {
        public TheSameUserForTheSameOrderException(string message) : base(message)
        {

        }
    }
}
