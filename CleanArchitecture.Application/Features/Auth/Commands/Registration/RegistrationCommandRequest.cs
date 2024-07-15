using MediatR;
using Microsoft.AspNetCore.Http;
using System.ComponentModel;

namespace CleanArchitecture.Application.Features.Auth.Commands.Registration
{
    public class RegistrationCommandRequest : IRequest<Unit>
    {
        public string FullName { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string ConfirmPassword { get; set; }
        public IFormFile Image { get; set; }
    }
}
