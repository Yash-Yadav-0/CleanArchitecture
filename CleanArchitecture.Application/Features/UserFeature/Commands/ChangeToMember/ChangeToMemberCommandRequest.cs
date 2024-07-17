using CleanArchitecture.Domain.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanArchitecture.Application.Features.UserFeature.Commands.ChangeToMember
{
    public class ChangeToMemberCommandRequest : IRequest<ChangeToMemberCommandResponse>
    {
        [DefaultValue("Enter User Email for (Vendor to User)")]
        public string Email {  get; set; }
    }
}
