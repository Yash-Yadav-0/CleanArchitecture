using CleanArchitecture.Application.Bases;

namespace CleanArchitecture.Application.Features.Auth.Exceptions
{
    public class EmailShouldnotbeInvalidException : BaseException
    {
        public EmailShouldnotbeInvalidException(string message) : base(message) { }

    }
}
