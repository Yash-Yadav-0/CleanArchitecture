using CleanArchitecture.Application.Bases;

namespace CleanArchitecture.Application.Features.Auth.Exceptions
{
    public class UserShouldBeFoundException : BaseException
    {
        public UserShouldBeFoundException(string message) : base(message) { }
    }
}
