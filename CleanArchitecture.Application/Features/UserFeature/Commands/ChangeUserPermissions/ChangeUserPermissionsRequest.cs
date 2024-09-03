using CleanArchitecture.Domain.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanArchitecture.Application.Features.UserFeature.Commands.ChangeUserPermissions
{
    public class ChangeUserPermissionsRequest : IRequest<ChangeUserPermissionsResponse>
    {
        public string Email { get; set; }
        public Permissions permissions { get; set; }
    }
}
