using CleanArchitecture.Application.Bases;

namespace CleanArchitecture.Application.Features.Orders.Exceptions
{
    public class TheSameUserForTheSameOrderException : BaseException
    {
        public TheSameUserForTheSameOrderException(string message) : base(message)
        {
        }
    }
}
