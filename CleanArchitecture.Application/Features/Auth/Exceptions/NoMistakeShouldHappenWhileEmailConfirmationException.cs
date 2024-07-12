using CleanArchitecture.Application.Bases;

namespace CleanArchitecture.Application.Features.Auth.Exceptions
{
    public class NoMistakeShouldHappenWhileEmailConfirmationException : BaseException
    {
        public NoMistakeShouldHappenWhileEmailConfirmationException(string message) : base(message) { }
    }
}
