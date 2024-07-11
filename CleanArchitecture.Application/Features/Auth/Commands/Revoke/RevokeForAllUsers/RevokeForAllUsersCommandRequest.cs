using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanArchitecture.Application.Features.Auth.Commands.Revoke.RevokeForAllUsers
{
    public class RevokeForAllUsersCommandRequest :IRequest<Unit>
    {
    }
}
