using MediatR;
using System.ComponentModel;


namespace CleanArchitecture.Application.Features.Auth.Commands.Revoke.RevokeForUser
{
    public class RevokeForUserCommandRequest : IRequest<Unit>
    {
        [DefaultValue("Your Email")]
        public string Email { get; set; }
    }
}
