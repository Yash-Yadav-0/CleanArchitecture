using CleanArchitecture.Application.Dtos.Auth.Email;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanArchitecture.Application.Features.Auth.Commands.Registration
{
    public class RegistrationCommandResponse
    {
        public EmailConfirmationDTO EmailConfirmationDTO { get; set; }
    }
}
