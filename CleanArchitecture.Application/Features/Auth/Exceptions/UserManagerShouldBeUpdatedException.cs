using CleanArchitecture.Application.Bases;

namespace CleanArchitecture.Application.Features.Auth.Exceptions
{
    public class UserManagerShouldBeUpdatedException : BaseException
    {
        public UserManagerShouldBeUpdatedException(string message) : base(message) { }
    }
}
