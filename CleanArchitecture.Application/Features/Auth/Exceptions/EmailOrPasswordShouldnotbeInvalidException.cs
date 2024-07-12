using CleanArchitecture.Application.Bases;

namespace CleanArchitecture.Application.Features.Auth.Exceptions
{
    public class EmailOrPasswordShouldnotbeInvalidException : BaseException
    {
        public EmailOrPasswordShouldnotbeInvalidException(string message) : base(message) { }
    }
}
