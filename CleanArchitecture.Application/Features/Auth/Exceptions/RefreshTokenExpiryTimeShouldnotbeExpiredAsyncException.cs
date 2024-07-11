using CleanArchitecture.Application.Bases;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanArchitecture.Application.Features.Auth.Exceptions
{
    public class RefreshTokenExpiryTimeShouldnotbeExpiredAsyncException : BaseException
    {
        public RefreshTokenExpiryTimeShouldnotbeExpiredAsyncException(string message) : base(message) { }
        public RefreshTokenExpiryTimeShouldnotbeExpiredAsyncException() : base() { }
    }
}
