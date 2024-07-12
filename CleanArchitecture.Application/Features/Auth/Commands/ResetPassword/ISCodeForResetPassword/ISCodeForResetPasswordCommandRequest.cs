using MediatR;

namespace CleanArchitecture.Application.Features.Auth.Commands.ResetPassword.ISCodeForResetPassword
{
    public class ISCodeForResetPasswordCommandRequest : IRequest<Unit>
    {
        public string Email { get; set; }

        public string CodeOfConfirm { get; set; }
    }
}
