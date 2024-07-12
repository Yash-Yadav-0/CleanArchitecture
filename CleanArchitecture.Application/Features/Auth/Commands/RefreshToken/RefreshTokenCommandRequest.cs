using MediatR;
using System.ComponentModel;

namespace CleanArchitecture.Application.Features.Auth.Commands.RefreshToken
{
    public class RefreshTokenCommandRequest : IRequest<RefreshTokenCommandResponse>
    {
        [DefaultValue("Enter AccessToken")]
        public string AccessToken { get; set; }

        [DefaultValue("Enter RefreshToken")]
        public string RefreshToken { get; set; }
    }
}
