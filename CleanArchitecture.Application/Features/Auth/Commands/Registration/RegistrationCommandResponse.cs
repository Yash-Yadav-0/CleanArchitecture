using CleanArchitecture.Application.Dtos.Auth.Email;

namespace CleanArchitecture.Application.Features.Auth.Commands.Registration
{
    public class RegistrationCommandResponse
    {
        public EmailConfirmationDTO EmailConfirmationDTO { get; set; }
    }
}
