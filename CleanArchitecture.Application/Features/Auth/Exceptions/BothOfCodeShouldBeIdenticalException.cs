using CleanArchitecture.Application.Bases;

namespace CleanArchitecture.Application.Features.Auth.Exceptions
{
    public class BothOfCodeShouldBeIdenticalException : BaseException
    {
        public BothOfCodeShouldBeIdenticalException(string message) : base(message) { }
    }
}
