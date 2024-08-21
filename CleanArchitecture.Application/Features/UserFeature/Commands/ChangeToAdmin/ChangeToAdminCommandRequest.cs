using MediatR;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanArchitecture.Application.Features.UserFeature.Commands.ChangeToAdmin
{
    public class ChangeToAdminCommandRequest : IRequest<ChangeToAdminCommandResponse>
    {
        [DefaultValue("Enter User Email for (Changing to Admin)")]
        public string Email { get; set; }
    }
}
