using MediatR;

namespace CleanArchitecture.Application.Features.Auth.Commands.ResetPassword.SendForResetPassword
{
    public class SendForResetPasswordCommandsRequest : IRequest<Unit>
    {
        public string Email { get; set; }
    }
}
