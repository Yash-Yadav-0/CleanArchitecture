using CleanArchitecture.Application.Bases;

namespace CleanArchitecture.Application.Features.Auth.Exceptions
{
    public class NotAllowedResetPasswordBeforeConfirmException : BaseException
    {
        public NotAllowedResetPasswordBeforeConfirmException(string message) : base(message) { }
    }
}
