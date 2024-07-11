using CleanArchitecture.Application.Bases;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanArchitecture.Application.Features.Auth.Exceptions
{
    public class EmailShouldBeConfirmedException : BaseException
    {
        public EmailShouldBeConfirmedException(string message) : base(message) { }
    }
}
