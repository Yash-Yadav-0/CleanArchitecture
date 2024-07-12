using CleanArchitecture.Application.Bases;

namespace CleanArchitecture.Application.Features.Auth.Exceptions
{
    public class UserAlreadyExistsException : BaseException
    {
        public UserAlreadyExistsException(string message) : base(message) { }
    }
}
