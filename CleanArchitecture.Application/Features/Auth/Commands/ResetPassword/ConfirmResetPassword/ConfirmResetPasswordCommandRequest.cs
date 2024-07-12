using MediatR;

namespace CleanArchitecture.Application.Features.Auth.Commands.ResetPassword.ConfirmResetPassword
{
    public class ConfirmResetPasswordCommandRequest : IRequest<Unit>
    {
        public string Email { get; set; }

        public string Password { get; set; }

        public string ConfirmPassword { get; set; }
    }
}
