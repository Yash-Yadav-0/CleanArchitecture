using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanArchitecture.Application.Dtos.Auth.Email
{
    public class EmailConfirmationDTO
    {
        public string Email { get; set; }
        public string Token { get; set; }

    }
}
