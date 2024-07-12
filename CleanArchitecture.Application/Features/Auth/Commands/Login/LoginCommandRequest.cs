using MediatR;
using System.ComponentModel;

namespace CleanArchitecture.Application.Features.Auth.Commands.Login
{
    public record LoginCommandRequest : IRequest<LoginCommandResponse>
    {

        [DefaultValue("Enter Your Email")]
        public string Email { get; set; }



        [DefaultValue("Enter Your Password")]
        public string Password { get; set; }
    }
}
