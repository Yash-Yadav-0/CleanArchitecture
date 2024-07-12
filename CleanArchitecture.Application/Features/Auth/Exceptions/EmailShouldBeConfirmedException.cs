using CleanArchitecture.Application.Bases;

namespace CleanArchitecture.Application.Features.Auth.Exceptions
{
    public class EmailShouldBeConfirmedException : BaseException
    {
        public EmailShouldBeConfirmedException(string message) : base(message) { }
    }
}
